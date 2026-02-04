import { useEffect, useState } from "react";
import Login from "./pages/Login/Login";
import FakeUsers from "./pages/FakeUsers/FakeUsers";

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    localStorage.removeItem("token");
  }, []);

  return isAuthenticated ? (
    <FakeUsers />
  ) : (
    <Login onLogin={() => setIsAuthenticated(true)} />
  );
}
