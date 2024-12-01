namespace Ch1_L14.Test
{
    public class ProgramTests
    {
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
    }
}