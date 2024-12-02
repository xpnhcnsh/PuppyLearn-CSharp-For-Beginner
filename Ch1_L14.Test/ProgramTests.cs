
namespace Ch1_L14.Test
{
    public class ProgramTests
    {
        #region Calculator����
       
        /// <summary>
        /// û�в����Ĳ���������ע[Fact]��
        /// </summary>
        [Fact]
        public void Given_Two_Int_Return_Sum()
        {
            //Arrange
            int i = 5;
            int j = 6;

            //Act
            var sut = new Calculator(); //sut: system under test,��ʾ�����ԵĶ���
            int actualResult = sut.Add(i, j);

            //Assert
            int expectedResult = 11;
            Assert.Equal(expectedResult, actualResult);
        }

        /// <summary>
        /// �����������
        /// </summary>
        [Fact]
        public void Given_Two_Int_Return_Quotient_Case1()
        {
            //Arrange
            int i = 10;
            int j = 2;

            //Act
            var sut = new Calculator(); //sut: system under test,��ʾ�����ԵĶ���
            int actualResult = sut.Divide(i, j);

            //Assert
            int expectedResult = 5;
            Assert.Equal(expectedResult, actualResult);
        }

        /// <summary>
        /// ���ֲ���·����ʹ��[Theory] attribute�����㡣
        /// 5���ֲ�ͬ���������<2.5ʱ��ȡ2����=2.5ʱ��ȡ3����>1.5ʱ��ȡ2������������Ϊ0��
        /// ��������������Ժ���case1����case3��Ҫ�������ԡ�
        /// </summary>
        [Theory]
        [InlineData(9, 4, 2)]
        [InlineData(10, 4, 3)]
        [InlineData(10, 6, 2)]
        [InlineData(10, 2, 5)]
        [InlineData(0, 2, 0)]
        public void Given_Two_Int_Return_Quotient_Case2(int i, int j, int expectedResult)
        {
            //Arrange ����Ҫ��

            //Act
            var sut = new Calculator(); //sut: system under test,��ʾ�����ԵĶ���
            int actualResult = sut.Divide(i, j);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        /// <summary>
        /// ������Ϊ0ʱ�׳��쳣
        /// </summary>
        [Fact]
        public void Given_Two_Int_Return_Quotient_Case3()
        {
            //Arrange
            int i = 9;
            int j = 0;

            //Act
            var sut = new Calculator(); //sut: system under test,��ʾ�����ԵĶ���

            //Assert
            Assert.Throws<DivideByZeroException>(() => sut.Divide(i, j));

        }

        #endregion

        # region Change User Password����

        /// <summary>
        /// ������ȷ��email��old password���޸�����ɹ�
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
        /// ���������email���޷��ҵ��û��������׳��쳣����messageΪEmail not found!
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
        /// ������ȷ��email�ʹ����old password
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
        /// ������ȷ��email����ȷ��old password����updateʱ��������
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
    /// ����Change User Password������
    /// FakeUserRepository�ǲ�����Ա�Լ�д���࣬������ģ��UserRepository�࣬��֤UserService������ȷ��ע���������
    /// �ڷ����У���Ҫ��֤����ʱ��������÷��������в���·����
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
            //���email�ﺬ��found����ʾemail��ȷ�������ʾδ�ҵ�email
            if (Email.Contains("found"))
                return Task.FromResult(new User { Email = Email, Password = _password });
            else
                return Task.FromResult<User?>(null);
        }

        Task<bool> IUserRepository.UpdateAsync(User user, CancellationToken cancellation)
        {
            //���email�а���true����ʾ�ɹ�update�������ʾupdateʧ��
            if(user.Email!.Contains("true"))
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
    }
}