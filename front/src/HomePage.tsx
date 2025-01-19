import React from "react";
import { Box, Button, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthProvider";

const HomePage = () => {
    const { user } = useAuth();
    const navigate = useNavigate();

    const handleLogin = () => {
        navigate("/login");
    };

    const handleRegister = () => {
        navigate("/register");
    };

    const handlePosts = () => {
        navigate("/posts");
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
            }}
        >
            <Typography variant="h4" gutterBottom>
                Witaj w aplikacji social media dla Ciebie i Twoich przyjaciół.
            </Typography>
            {user ? (
                <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
                    <Typography variant="h6">Cześć, {user.firstName}!</Typography>
                    <Button variant="contained" color="primary" onClick={handlePosts}>
                        Przejdź do postów
                    </Button>
                </Box>
            ) : (
                <Box sx={{ display: "flex", gap: 2 }}>
                    <Button variant="outlined" onClick={handleLogin}>
                        Zaloguj się
                    </Button>
                    <Button variant="contained" onClick={handleRegister}>
                        Zarejestruj się
                    </Button>
                </Box>
            )}
        </Box>
    );
};

export default HomePage;
