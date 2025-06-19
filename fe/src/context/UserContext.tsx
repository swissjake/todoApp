import { createContext, useState, useEffect, useContext } from "react";

export interface User {
  id: string;
  email: string;
  name: string;
  isLoading: boolean;
}

interface UserContextType {
  user: User | null;
  handleSetUser: (user: User | null) => void;
  logout: () => void;
  isLoading: boolean;
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export const UserProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  console.log({ user });

  useEffect(() => {
    // Check for stored user data on mount
    const storedUser = localStorage.getItem("user");
    const storedTokens = localStorage.getItem("tokens");

    if (storedUser && storedTokens) {
      try {
        setUser(JSON.parse(storedUser));
      } catch (error) {
        console.error("Error parsing stored user data:", error);
        localStorage.removeItem("user");
        localStorage.removeItem("tokens");
      }
    }
    setIsLoading(false);
  }, []);

  const handleSetUser = (user: User | null) => {
    setUser(user);
    if (user) {
      localStorage.setItem("user", JSON.stringify(user));
    } else {
      localStorage.removeItem("user");
      localStorage.removeItem("tokens");
    }
  };

  const logout = () => {
    handleSetUser(null);
    localStorage.removeItem("tokens");
  };

  return (
    <UserContext.Provider value={{ user, handleSetUser, logout, isLoading }}>
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => {
  const context = useContext(UserContext);
  if (context === undefined) {
    throw new Error("useUser must be used within a UserProvider");
  }
  return context;
};

export default UserContext;
