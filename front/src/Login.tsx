import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthProvider";
import Button from '@mui/material/Button';
import { Box, TextField } from "@mui/material";
import axios from "axios";

const Login = () => {
    const { setToken, setUser } = useAuth();
    const navigate = useNavigate();

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState<string | null>(null);

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault(); 

        try {
            const response = await axios.post("http://localhost:5000/User/login", {
                username,
                password
            });

            const { token, ...userResponseData } = response.data;
            setToken(token);
            setUser(userResponseData);

            navigate("/", { replace: true });
        } catch (err: any) {
            setError("Invalid username or password");
        }
    };

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                alignItems: "center",
                height: "100vh",
                backgroundColor: "#f5f5f5"
            }}
        >
            <h2>Zaloguj się</h2>
            <form onSubmit={handleLogin}>
                <div>
                    <TextField
                        label="Nazwa użytkownika"
                        variant="outlined"
                        type="text"
                        id="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                        sx={{ marginBottom: "16px" }}
                    />
                </div>
                <div>
                    <TextField
                        label="Hasło"
                        variant="outlined"
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                        sx={{ marginBottom: "16px" }}
                    />
                </div>
                {error && <p style={{ color: "red" }}>{error}</p>}
                <Button type="submit" variant="contained" fullWidth>Zaloguj się</Button>
            </form>
        </Box>
    );
};

export default Login;
