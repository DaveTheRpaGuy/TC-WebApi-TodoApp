create procedure [dbo].[spTodos_UpdateTask]
	@TodoId int,
	@AssignedTo int,
	@Task nvarchar(50)
as
begin
	update dbo.Todos
	set Task = @Task
	where Id = @TodoId
		and AssignedTo = @AssignedTo;
end