import api from "@/lib/axios";
import { Button } from "../button";
import { format } from "date-fns";
import { toast } from "sonner";

export interface Todo {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt: string;
}

const TodoList = ({
  todos,

  fetchTodos,
  handleComplete,

  setEdit,
}: {
  todos: Todo[];

  fetchTodos: () => void;
  handleComplete: (todo: Todo) => void;
  setEdit: (edit: Todo | null) => void;
}) => {
  const handleDelete = async (todo: Todo) => {
    const response = await api.delete(`/Todos/${todo.id}`);
    if (response.status === 204) {
      toast.success("Todo deleted successfully");
      fetchTodos();
    }
  };

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
      {todos.map((todo) => (
        <div
          className="border-2 border-gray-300 rounded-md p-2 bg-purple-100"
          key={todo.id}
        >
          <h3 className="text-lg font-bold">{todo.title}</h3>
          <p className="text-sm text-gray-500 break-words">
            {todo.description}
          </p>

          <small className="text-sm text-gray-500">
            <span className="font-bold">Created at:</span>{" "}
            {format(new Date(todo.createdAt), "dd/MM/yyyy HH:mm:ss")}
          </small>

          {todo.isCompleted ? (
            <p className="text-sm text-green-500">Completed</p>
          ) : (
            <p className="text-sm text-red-500">Incomplete</p>
          )}

          <Button
            onClick={() => handleDelete(todo)}
            className="w-full bg-red-800 p-1 mt-8"
          >
            Delete
          </Button>
          <Button
            onClick={() => {
              setEdit(todo);
            }}
            className="w-full bg-green-800 p-1 mt-2"
          >
            Edit
          </Button>
          <Button
            onClick={() => handleComplete(todo)}
            className="w-full bg-green-800 p-1 mt-2"
          >
            Complete
          </Button>
        </div>
      ))}
    </div>
  );
};

export default TodoList;
