import "./App.css";
import Routes from "./components/routes";
import { useUser } from "./context/UserContext";

function App() {
  const { isLoading } = useUser();

  return isLoading ? <div>Loading...</div> : <Routes />;
}

export default App;
