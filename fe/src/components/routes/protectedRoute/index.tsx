import { Navigate, Outlet } from "react-router-dom";
import { useUser } from "@/context/UserContext";
const ProtectedRoute = () => {
  const { user } = useUser();
  console.log({ user });
  return !user ? <Navigate to="/" /> : <Outlet />;
};

export default ProtectedRoute;
