using MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;

namespace MarketManagement.Domain.UnitTests.ShoppingCartTests.ValueObjectsTests;

public class AmountTests
{
    [Fact]
    public void AmountInit_WhenValidValue_ShouldReturnsAmount()
    {
        // Arrange
        var initValue = 5;
        var expectedAmountValue = 5;

        // Act
        var amountVo = Amount.AmountFor(initValue);

        // Assert
        Assert.NotNull(amountVo);
        Assert.Equal(expectedAmountValue, amountVo.GetValue());
    }

    [Fact]
    public void AmountInit_WhenInvalidValidValue_ShouldThrowsArgumentException()
    {
        // Arrange
        var initValue = -5;
        var expectedExceptionMessae = "Le montant doit être supérieur à 0";

        // Assert
        var exception = Assert.Throws<ArgumentException>(() => Amount.AmountFor(initValue));
        Assert.Equal(expectedExceptionMessae, exception.Message);
    }

    [Fact]
    public void AmountAdd_WhenAddingTwoAmount_ShouldReturnsNewAmountWithSum()
    {
        // Arrange
        var amountVo1 = Amount.AmountFor(5);
        var amountVo2 = Amount.AmountFor(2);
        var expectedSumValue = 7;

        // Act
        var sumAmount = amountVo1+ amountVo2;

        // Assert
        Assert.NotNull(sumAmount);
        Assert.Equal(expectedSumValue, sumAmount.GetValue());
    }

    [Fact]
    public void AmountAdd_WhenSubstractTwoAmount_ShouldReturnsNewAmountWithSum()
    {
        // Arrange
        var amountVo1 = Amount.AmountFor(10);
        var amountVo2 = Amount.AmountFor(2);
        var expectedSumValue = 8;

        // Act
        var sumAmount = amountVo1 - amountVo2;

        // Assert
        Assert.NotNull(sumAmount);
        Assert.Equal(expectedSumValue, sumAmount.GetValue());
    }

    [Fact]
    public void AmountMultiply_WhenSecondAmountISNull_ShouldReturnsAmountWithMultplyValue()
    {
        // Arrange
        var amountVo1 = Amount.AmountFor(10);
        var amountVo2 = Amount.AmountFor(2);
        var expectedSumValue = 20;

        // Act
        var sumAmount = amountVo1 * amountVo2;

        // Assert
        Assert.NotNull(sumAmount);
        Assert.Equal(expectedSumValue, sumAmount.GetValue());
    }

    [Fact]
    public void AmountAdd_WhenSecondAmountISNull_ShouldReturnsFirstAmountValue()
    {
        // Arrange
        var amountVo1 = Amount.AmountFor(8);
        Amount amountVo2 = null!;
        var expectedSumValue = 8;

        // Act
        var sumAmount = amountVo1 + amountVo2;

        // Assert
        Assert.NotNull(sumAmount);
        Assert.Equal(expectedSumValue, sumAmount.GetValue());
    }


    /// <remarks>
    /// A disccuter avec le métier !!!
    /// </remarks>
    [Fact]
    public void AmountAdd_WhenSubstractSmallerAmountWithBiggerAmountAmount_ShouldThrowsArgumentException()
    {
        // Arrange
        var amountVo1 = Amount.AmountFor(1);
        var amountVo2 = Amount.AmountFor(2);
        var expectedExceptionMessae = "Le montant doit être supérieur à 0";


        // Assert
        var exception = Assert.Throws<ArgumentException>(() => amountVo1 - amountVo2);
        Assert.Equal(expectedExceptionMessae, exception.Message);
    }
}

