CREATE PROCEDURE [dbo].[spTodos_Create]
	@Task nvarchar(50),
	@AssignedTo int
as
begin
	insert into dbo.Todos (Task, AssignedTo)
	values (@Task, @AssignedTo);

	select Id, Task, AssignedTo, IsComplete
	from dbo.Todos
	where Id = scope_identity(); -- @@Identity
end