import TodoList, { type Todo } from "./todoList";
import { Button } from "../button";
import { useState, useEffect } from "react";
import { Textarea } from "../textArea";
import api from "@/lib/axios";
import { toast } from "sonner";
import { useUser } from "@/context/UserContext";
import { Checkbox } from "../checkbox";
import { Loader2 } from "lucide-react";

const TodoComponent = () => {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [edit, setEdit] = useState<Todo | null>(null);
  const { user } = useUser();
  const [addTodo, setAddTodo] = useState({
    title: "",
    description: "",
    isLoading: false,
    isCompleted: false,
  });

  // Fetch todos on component mount
  useEffect(() => {
    fetchTodos();
  }, []);

  const fetchTodos = async () => {
    try {
      const response = await api.get("/Todos");
      setTodos(response.data);
    } catch (error) {
      console.error("Error fetching todos:", error);
    }
  };

  const handleAddTodo = async () => {
    setAddTodo({ ...addTodo, isLoading: true });
    try {
      const response = await api.post("/Todos", {
        title: addTodo.title,
        description: addTodo.description,
      });

      if (response.status === 201) {
        toast.success("Todo added successfully");
        setAddTodo({
          title: "",
          description: "",
          isLoading: false,
          isCompleted: false,
        });
        // Refresh the todo list immediately
        fetchTodos();
      }
    } catch (error) {
      console.error("Error adding todo:", error);
      setAddTodo({ ...addTodo, isLoading: false });
    }
  };

  const handleEdit = async () => {
    const input = document.getElementById("todo-input") as HTMLInputElement;
    input.focus();
    try {
      const response = await api.put(`/Todos/${edit?.id}`, {
        title: addTodo.title,
        description: addTodo.description,
        isCompleted: addTodo.isCompleted,
      });

      if (response.status === 200) {
        toast.success("Todo updated successfully");
        // Refresh the todo list immediately
        fetchTodos();
        setEdit(null);
        setAddTodo({
          title: "",
          description: "",
          isLoading: false,
          isCompleted: false,
        });
      }
    } catch (error) {
      console.error("Error updating todo:", error);
    }
  };

  const handleComplete = async (todo: Todo) => {
    const response = await api.put(`/Todos/${todo.id}/complete`);
    if (response.status === 200) {
      toast.success("Todo completed successfully");
      fetchTodos();
    }
  };

  return (
    <div className="flex flex-col gap-4 max-w-2xl w-full mx-auto py-10 px-5">
      <h2 className="text-2xl font-bold">Welcome back, {user?.name}</h2>
      <div className="flex w-full flex-col gap-4 ">
        <h2 className="text-2xl font-bold">Your Todo List</h2>
        <div className="flex flex-col w-full gap-2 justify-center">
          <input
            id="todo-input"
            className="border-2 w-full border-gray-300 rounded-md p-2"
            type="text"
            placeholder="Add a new todo"
            value={addTodo.title}
            onChange={(e) => setAddTodo({ ...addTodo, title: e.target.value })}
          />
          <Textarea
            placeholder="Description"
            value={addTodo.description}
            onChange={(e) =>
              setAddTodo({ ...addTodo, description: e.target.value })
            }
          />

          <Checkbox
            checked={addTodo.isCompleted}
            onCheckedChange={(checked: boolean) =>
              setAddTodo({ ...addTodo, isCompleted: checked })
            }
          />
          <Button
            className="w-full p-2"
            disabled={
              !addTodo.title || !addTodo.description || addTodo.isLoading
            }
            onClick={() => {
              if (edit) {
                handleEdit();
              } else {
                handleAddTodo();
              }
            }}
          >
            {addTodo.isLoading ? "Adding..." : "Add"}
          </Button>
        </div>
      </div>

      {addTodo.isLoading ? (
        <div className="flex justify-center items-center">
          <Loader2 className="w-4 h-4 animate-spin" />
        </div>
      ) : (
        <TodoList
          setEdit={setEdit}
          todos={todos}
          fetchTodos={fetchTodos}
          handleComplete={handleComplete}
        />
      )}
    </div>
  );
};

export default TodoComponent;
