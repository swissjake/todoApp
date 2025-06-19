import { BrowserRouter, Routes as RouterRoutes, Route } from "react-router-dom";
import RegisterPage from "@/pages/register";
import LoginPage from "@/pages/login";
import TodoPage from "@/pages/todo";
import ProtectedRoute from "./protectedRoute";
import PublicRoutes from "./publicRoutes";

const Routes = () => {
  return (
    <BrowserRouter>
      <RouterRoutes>
        <Route element={<PublicRoutes />}>
          <Route path="*" element={<LoginPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
        </Route>

        <Route element={<ProtectedRoute />}>
          <Route path="/todo" element={<TodoPage />} />
        </Route>
      </RouterRoutes>
    </BrowserRouter>
  );
};

export default Routes;
