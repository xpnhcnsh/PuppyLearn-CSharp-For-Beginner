
namespace Ch1_L14.Test
{
    public class ProgramTests
    {
        #region Calculator案例
       
        /// <summary>
        /// 没有参数的测试用例标注[Fact]。
        /// </summary>
        [Fact]
        public void Given_Two_Int_Return_Sum()
        {
            //Arrange
            int i = 5;
            int j = 6;

            //Act
            var sut = new Calculator(); //sut: system under test,表示待测试的对象
            int actualResult = sut.Add(i, j);

            //Assert
            int expectedResult = 11;
            Assert.Equal(expectedResult, actualResult);
        }

        /// <summary>
        /// 能整除的情况
        /// </summary>
        [Fact]
        public void Given_Two_Int_Return_Quotient_Case1()
        {
            //Arrange
            int i = 10;
            int j = 2;

            //Act
            var sut = new Calculator(); //sut: system under test,表示待测试的对象
            int actualResult = sut.Divide(i, j);

            //Assert
            int expectedResult = 5;
            Assert.Equal(expectedResult, actualResult);
        }

        /// <summary>
        /// 多种测试路径，使用[Theory] attribute更方便。
        /// 5种种不同的情况：当<2.5时，取2；当=2.5时，取3；当>1.5时，取2；整除，除数为0。
        /// 这个测试用例可以涵盖case1，但case3需要单独测试。
        /// </summary>
        [Theory]
        [InlineData(9, 4, 2)]
        [InlineData(10, 4, 3)]
        [InlineData(10, 6, 2)]
        [InlineData(10, 2, 5)]
        [InlineData(0, 2, 0)]
        public void Given_Two_Int_Return_Quotient_Case2(int i, int j, int expectedResult)
        {
            //Arrange 不需要了

            //Act
            var sut = new Calculator(); //sut: system under test,表示待测试的对象
            int actualResult = sut.Divide(i, j);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        /// <summary>
        /// 被除数为0时抛出异常
        /// </summary>
        [Fact]
        public void Given_Two_Int_Return_Quotient_Case3()
        {
            //Arrange
            int i = 9;
            int j = 0;

            //Act
            var sut = new Calculator(); //sut: system under test,表示待测试的对象

            //Assert
            Assert.Throws<DivideByZeroException>(() => sut.Divide(i, j));

        }

        #endregion

        # region Change User Password案例

        /// <summary>
        /// 给定正确的email和old password，修改密码成功
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Change_Password_By_Email_Case1()
        {
            //Arrange
            string Email = "found@true.com";
            string oldPsw = "123456";
            string newPsw = "123";

            //Act
            UserService sut = new UserService(new FakeUserRepository("123456"));
            bool actualResult = await sut.ChangePasswordAsync(Email, oldPsw, newPsw, CancellationToken.None);

            //Assert
            Assert.True(actualResult);
        }

        /// <summary>
        /// 给定错误的email，无法找到用户，期望抛出异常，且message为Email not found!
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void Change_Password_By_Email_Case2()
        {
            //Arrange
            string Email = "notFound@true.com";
            string oldPsw = "123456";
            string newPsw = "123";

            //Act
            UserService sut = new UserService(new FakeUserRepository("123456"));
            var actualResult = Assert.ThrowsAsync<Exception>(() => sut.ChangePasswordAsync(Email, oldPsw, newPsw, CancellationToken.None)).Result.Message;
            string expectedResult = "Email not found!";

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        /// <summary>
        /// 给定正确的email和错误的old password
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void Change_Password_By_Email_Case3()
        {
            //Arrange
            string Email = "found@true.com";
            string oldPsw = "111111";
            string newPsw = "123";

            //Act
            UserService sut = new UserService(new FakeUserRepository("123456"));
            
            bool actualResult = await sut.ChangePasswordAsync(Email, oldPsw, newPsw, CancellationToken.None);

            //Assert
            Assert.False(actualResult);
        }

        /// <summary>
        /// 给定正确的email和正确的old password，但update时发生错误
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void Change_Password_By_Email_Case4()
        {
            //Arrange
            string Email = "found@false.com";
            string oldPsw = "123456";
            string newPsw = "123";

            //Act
            UserService sut = new UserService(new FakeUserRepository("123456"));

            bool actualResult = await sut.ChangePasswordAsync(Email, oldPsw, newPsw, CancellationToken.None);

            //Assert
            Assert.False(actualResult);
        }
        #endregion
    }

    /// <summary>
    /// 用于Change User Password案例。
    /// FakeUserRepository是测试人员自己写的类，作用是模拟UserRepository类，保证UserService可以正确的注入该依赖。
    /// 在方法中，需要保证测试时，可以穷尽该方法的所有测试路径。
    /// </summary>
    public class FakeUserRepository : IUserRepository
    {
        private string _password;

        public FakeUserRepository(string password)
        {
            _password = password;
        }

        Task<User?> IUserRepository.GetByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            //如果email里含有found，表示email正确，否则表示未找到email
            if (Email.Contains("found"))
                return Task.FromResult(new User { Email = Email, Password = _password });
            else
                return Task.FromResult<User?>(null);
        }

        Task<bool> IUserRepository.UpdateAsync(User user, CancellationToken cancellation)
        {
            //如果email中包含true，表示成功update，否则表示update失败
            if(user.Email!.Contains("true"))
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
    }
}