create procedure [dbo].[spTodos_CompleteTodo]
	@TodoId int,
	@AssignedTo int
as
begin
	update dbo.Todos
	set IsComplete = 1
	where Id = @TodoId
		and AssignedTo = @AssignedTo;
end