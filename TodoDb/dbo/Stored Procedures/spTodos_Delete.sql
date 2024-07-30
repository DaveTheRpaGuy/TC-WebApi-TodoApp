create procedure [dbo].[spTodos_Delete]
	@TodoId int,
	@AssignedTo int
as
begin
	delete from dbo.Todos
	where Id = @TodoId
		and AssignedTo = @AssignedTo;
end