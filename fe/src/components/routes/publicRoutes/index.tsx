import { useUser } from "@/context/UserContext";
import { Navigate, Outlet } from "react-router-dom";

const PublicRoutes = () => {
  const { user } = useUser();
  return user ? <Navigate to="/todo" /> : <Outlet />;
};

export default PublicRoutes;
