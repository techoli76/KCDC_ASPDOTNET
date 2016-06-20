CREATE TRIGGER Store.UpdateOrderTotal
    ON [Store].[OrderDetails]
    FOR DELETE, INSERT, UPDATE
    AS
    BEGIN
		SET NOCOUNT ON
		DECLARE @OrderId INT;

		DECLARE new_records_cursor CURSOR FOR 
			SELECT DISTINCT OrderId FROM 
			(SELECT OrderId FROM INSERTED UNION 
			SELECT OrderId FROM DELETED) AS all_changes;
		OPEN new_records_cursor;
	
		FETCH NEXT FROM new_records_cursor INTO @OrderID;
		WHILE @@FETCH_STATUS = 0
		BEGIN 
			UPDATE Orders SET OrderTotalComputed = Store.GetOrderTotal(@OrderId) WHERE Id = @OrderID;
			FETCH NEXT FROM new_records_cursor INTO @OrderID;
		END;
		CLOSE new_records_cursor;
		DEALLOCATE new_records_cursor;

    END;