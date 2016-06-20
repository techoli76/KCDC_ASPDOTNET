-- =============================================
-- Author:		Philip Japikse
-- Create date: 
-- Description:	
-- =============================================
CREATE FUNCTION Store.GetOrderTotal 
(
	@OrderId int
)
RETURNS money
AS
BEGIN
	DECLARE @Result money

	SELECT @Result = SUM(LineItemTotal) FROM OrderDetails WHERE OrderId = @OrderId;

	RETURN @Result

END
GO
