import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthProvider";
import { Box, Typography } from "@mui/material";

const Logout = () => {
  const { setToken, setUser } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    setToken('');
    setUser(null);
    navigate("/", { replace: true });
  };

  setTimeout(() => {
    handleLogout();
  }, 3 * 1000);

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "100vh",
        backgroundColor: "#f5f5f5",
      }}
    >
      <Typography variant="h5" gutterBottom>
        Wylogowanie...
      </Typography>
      <Typography variant="body1">
        Zostałeś pomyślnie wylogowany. Zostaniesz przeniesiony na stronę główną.
      </Typography>
    </Box>
  );
};

export default Logout;
