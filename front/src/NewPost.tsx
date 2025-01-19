import React, { useState } from "react";
import { Avatar, Box, Button, Card, CardHeader, TextField, Typography } from "@mui/material";
import { useAuth } from "./AuthProvider";
import axios from "axios";

interface NewPostFormProps {
    onPostAdded: () => void; 
}

const NewPostForm = ({ onPostAdded }: NewPostFormProps) => {
    const [adding, setAdding] = useState(false);
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [image, setImage] = useState<string>("");
    const { user } = useAuth();

    const handleAddPost = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            await axios.post(`http://localhost:5073/post`,
                {
                    title, content: description, imageUrl: image
                });
            setAdding(false);
            setTitle("")
            setDescription("")
            setImage("")
            onPostAdded();

        } catch (error) {
            console.error("Error while removing reaction:", error);
        }
    };

    if (!adding) {
        return <Box sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            maxWidth: "600px",
            margin: "16px auto",
        }}><Card sx={{ width: "100%", }}>
                <CardHeader
                    avatar={
                        <Avatar sx={{ bgcolor: "red" }} aria-label="recipe">
                            {user?.firstName[0].toUpperCase()}
                        </Avatar>
                    }
                    title={<Button onClick={() => { setAdding(true) }}>Dodaj nowy post</Button>}
                /></Card></Box>;
    }

    return (
        <Box
            component="form"
            onSubmit={handleAddPost}
            sx={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
                maxWidth: "600px",
                margin: "16px auto",
            }}
        >
            <Typography variant="h6" gutterBottom>
                Jak Ci minął dzień?
            </Typography>
            <TextField
                label="Tytuł"
                variant="outlined"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
                fullWidth
                sx={{ marginBottom: 2 }}
            />
            <TextField
                label="Opis"
                variant="outlined"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                multiline
                rows={4}
                required
                fullWidth
                sx={{ marginBottom: 2 }}
            />
            <TextField
                label="URL obrazka"
                variant="outlined"
                value={image}
                onChange={(e) => setImage(e.target.value)}
                required
                fullWidth
                sx={{ marginBottom: 2 }}
            />
            <Button type="submit" variant="contained" color="primary" onClick={handleAddPost}>
                Dodaj post
            </Button>
        </Box>
    );

};

export default NewPostForm;
