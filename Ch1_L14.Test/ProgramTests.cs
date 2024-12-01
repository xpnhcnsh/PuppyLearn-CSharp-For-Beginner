namespace Ch1_L14.Test
{
    public class ProgramTests
    {
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
    }
}