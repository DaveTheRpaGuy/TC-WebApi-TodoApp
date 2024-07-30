create procedure [dbo].[spTodos_GetAllAssigned]
	@AssignedTo int
as
begin
	select Id, Task, AssignedTo, IsComplete
	from dbo.TodoDb
	where AssignedTo = @AssignedTo;
end
