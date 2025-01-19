import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthProvider";
import axios from "axios";
import { TextField, Button, Box } from "@mui/material";

interface UserRegister {
    userName: string;
    password: string;
    email: string;
    firstName: string;
    lastName: string;
}

const Register = () => {
    const { setToken, setUser } = useAuth();
    const navigate = useNavigate();

    const [userData, setUserData] = useState<UserRegister>({
        email: "",
        userName: "",
        password: "",
        firstName: "",
        lastName: "",
    });
    const [error, setError] = useState<string | null>(null);

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault(); 

        try {

            const response = await axios.post("http://localhost:5000/User/Register", userData);

            const { token, ...userResponseData } = response.data;

            setToken(token);
            setUser(userResponseData);

            navigate("/", { replace: true });
        } catch (err: any) {
            setError("Invalid registration details");
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setUserData((prev) => ({ ...prev, [name]: value }));
    };

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                alignItems: "center",
                height: "100vh",
                backgroundColor: "#f5f5f5",
                padding: "16px", 
            }}
        >
            <h2>Zarejestruj się</h2>
            <form
                onSubmit={handleRegister}
                style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: "16px",
                    width: "100%",
                    maxWidth: "400px",
                }}
            >
                <TextField
                    label="Username"
                    name="userName"
                    value={userData.userName}
                    onChange={handleInputChange}
                    required
                    fullWidth
                    variant="outlined"
                />
                <TextField
                    label="Password"
                    name="password"
                    type="password"
                    value={userData.password}
                    onChange={handleInputChange}
                    required
                    fullWidth
                    variant="outlined"
                />
                <TextField
                    label="Email"
                    name="email"
                    type="email"
                    value={userData.email}
                    onChange={handleInputChange}
                    required
                    fullWidth
                    variant="outlined"
                />
                <TextField
                    label="First Name"
                    name="firstName"
                    value={userData.firstName}
                    onChange={handleInputChange}
                    required
                    fullWidth
                    variant="outlined"
                />
                <TextField
                    label="Last Name"
                    name="lastName"
                    value={userData.lastName}
                    onChange={handleInputChange}
                    required
                    fullWidth
                    variant="outlined"
                />
                {error && <p style={{ color: "red" }}>{error}</p>}
                <Button
                    type="submit"
                    variant="contained"
                    fullWidth
                >
                    Zarejestruj się
                </Button>
            </form>
        </Box>
    );
};

export default Register;
