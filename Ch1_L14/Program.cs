

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
#endregion
Console.WriteLine();

#region Calculator
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
        return (int)Math.Round((double)a / (double)b, MidpointRounding.AwayFromZero);
    }
}
#endregion