import { useUser } from "@/context/UserContext";
import { Button } from "../button";
import { Input } from "../input";
import { useState } from "react";
import api from "@/lib/axios";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const { handleSetUser } = useUser();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      const response = await api.post("auth/login", {
        email,
        password,
      });

      // Store tokens
      localStorage.setItem(
        "tokens",
        JSON.stringify({
          accessToken: response.data.accessToken,
          refreshToken: response.data.refreshToken,
          accessTokenExpiresAt: response.data.accessTokenExpiresAt,
          refreshTokenExpiresAt: response.data.refreshTokenExpiresAt,
        })
      );

      // Store user
      handleSetUser(response.data.user);
      navigate("/todo");
      setIsLoading(false);
    } catch (error) {
      console.error("Login error:", error);
      setIsLoading(false);
    }
  };

  return (
    <div className="flex h-screen w-screen items-center justify-center">
      <div className="flex flex-col items-center justify-center w-full max-w-[400px]">
        <h1 className="text-2xl font-bold">Login</h1>
        <form
          onSubmit={handleSubmit}
          className="flex w-full flex-col items-center justify-center space-y-4 mt-4"
        >
          <Input
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            type="text"
            placeholder="Email"
          />
          <Input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            type="password"
            placeholder="Password"
          />
          <Button className="w-full" type="submit" disabled={isLoading}>
            {isLoading ? "Loading..." : "Login"}
          </Button>
        </form>
      </div>
    </div>
  );
};

export default Login;
