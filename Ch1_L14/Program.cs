//Test

#region 概念和步骤
//1. TDD (Test Driven Development)：测试驱动开发，通常指讲测试行为往前提，不是把软件全部开发完了再去测试，而是在每个单元开发完毕猴就单独测试这个单元，被称为单元测试(Unit Test)。
//2. 测试覆盖率：表示已被测试的代码，占总代码行数的比例。例如总代码1万行，已通过测试的代码是1千行，那么覆盖率就是10%（通常只有含有计算和逻辑的代码需要被测试，因此覆盖率不可能是100%）。
//   覆盖率也可以用其他指标表示，例如mutation score等。通常在PR被成功提交之前，会有自动化测试程序对提交的代码进行测试，覆盖率通常是其中的一个考核标准，例如PR被Permit的标准中要求覆盖率
//   最少达到30%，如果自动测试流程发现提交的代码中测试覆盖率不足，那么该PR不会被推送到Leader那里，而是直接告诉程序员需要添加更多测试代码再提交PR。
//3. 测试代码单独创建项目，命名方式是<待测试项目.Test>，表示这个测试项目只针对另一个项目的。例如本项目为Ch1_L14，那么针对本项目的测试项目命名为Ch1_L14.Test。
//4. 测试项目引用被测试项目。测试项目中文件名和被测试项目文件名前缀相同，后缀加Tests即可。
//5. 在测试项目中写Test case，每种测试路径都要有一个测试用例。
//6. 测试用例中，分三步进行测试：1. Arrange:准备测试数据；2. Act：执行测试方法，获得实际结果；3.Assert：对比期望结果和实际结果。
//7. 右键点击需要运行的测试用例，点击Run Tests进行测试，然后查看测试结果。
//8. 当待测试代码引用了其他代码，且这部分代码无法获得时，需要在test时人为添加fake或mock，用来替代这部分代码。（见Change User Password案例中的FakeUserRepository）
Console.WriteLine();
#endregion

#region Calculator 案例
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
        //假设代码出错了：
        //return a * b;
    }

    public int Divide(int a, int b)
    {
        //return a / b;

        //四舍五入取整：
        //return (int)Math.Round((double)a / (double)b, MidpointRounding.AwayFromZero);

        //b为0时需要抛出异常DivideByZeroException：
        if (b == 0)
            throw new DivideByZeroException();
        return (int)Math.Round(a / (double)b, MidpointRounding.AwayFromZero);
    }
}
#endregion

#region Change User Password 案例
public record User { public string? Email; public string? Password; };

public class UserService
{
    /// <summary>
    /// UserService依赖于IUserRepository，使用构造函数进行依赖注入。假设_userRepository由队友实现，
    /// 在编写UserService代码时，_userRepository还未完成，现在我们仅仅知道IUserRepository的内容
    /// </summary>
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// 待测试的方法。注意：这里默认email的格式全都正确，无需使用regex进行验证，因为通常这样的验证都在前端完成，当后端收到ChangePasswordAsync的请求时，
    /// 应当默认email的格式无误。
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="oldPsw"></param>
    /// <param name="newPsw"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> ChangePasswordAsync(string Email, string oldPsw, string newPsw, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(Email, cancellationToken);
        if (user is null)
            throw new Exception("Email not found!");
        if (!user.Password!.Equals(oldPsw))
            return false;
        user.Password = newPsw;
        return await _userRepository.UpdateAsync(user, cancellationToken);
    }

}

/// <summary>
/// 虽然在这个阶段UserRepository还未被实现，但是我们知道这个接口本身已经完成了，实际开发中，这个接口
/// 会在另一个项目或者文件里，这里为了简化和UserService写在了一起
/// </summary>
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string Email, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(User user, CancellationToken cancellation);
}
#endregion

#region Stryker：Mutation Test（变异测试）
//传统的单元测试中，只要测试代码运行通过就算覆盖到了，如果程序员在测试代码中不写任何的Assert，也同样能保证测试代码正常通过，但这样并没有起到测试的效果。
//意味着通过偷懒可以实现一个很高的覆盖率，但其实并没有真正测试代码。因此单纯考虑覆盖率并不能很好的反应测试代码的质量。
//Mutation Test会在执行测试代码之前，将测试代码进行一些修改，比如-变+，*变/等，然后再执行测试后，理论上应该测试不通过，如果测试通过了，意味着测试代码
//未测试某些逻辑分支。
//在Mutation Test中，被改变的代码被称为Mutants(变异体)，未通过测试的变异体被称为killed，通过测试的变异体被称为survivor，killed越多越好，survivor越少越好。

//1. 安装Stryker：https://stryker-mutator.io/docs/stryker-net/getting-started/，使用Install globally的方法。
//2. 在Ch1_L14.Test目录下执行dotnet stryker，执行前确保Ch1_L14.Test引用了Ch1_L14项目。如果需要额外配置，在Ch1_L14.Test目录下添加一个
//stryker-config.json文件。
//可以看到当前我们的Ch1_L14.Test的Mutation Score是90%。
#endregion