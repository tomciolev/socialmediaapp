import { useEffect, useState } from "react";
import { Box, Button, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import Post from "./Post";
import axios from "axios";
import NewPostForm from "./NewPost";

export interface UserDto {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    userName: string;
}
export interface PostProps {
    id: string;
    title: string;
    content: string;
    imageUrl: string;
    createdAt: string;
    user: UserDto;
    reactions: Reaction[];
}

export interface Reaction {
    id: string;
    emoji: string;
    user: UserDto;
}

const PostList = () => {
    const [posts, setPosts] = useState<PostProps[]>([]);
    const navigate = useNavigate();

    const fetchPosts = async () => {
        try {
            const response = await axios.get("http://localhost:5073/Post");
            setPosts(response.data);
        } catch (error) {
            console.error("Error fetching posts:", error);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    const handlePostAddedRemovedChanged = () => {
        fetchPosts();
    };

    const handleLogout = () => {
        navigate("/logout");
    };

    return (
        <Box>
            <Box
                sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                    padding: "16px",
                    backgroundColor: "#f5f5f5",
                    borderBottom: "1px solid #ddd",
                }}
            >
                <Typography variant="h6" sx={{ fontWeight: "bold" }}>
                    Wspomnienia przyjaciół
                </Typography>
                <Button variant="outlined" color="primary" onClick={handleLogout}>
                    Wyloguj się
                </Button>
            </Box>

            <NewPostForm onPostAdded={handlePostAddedRemovedChanged} />
            <Box>
                {posts.map((post) => (
                    <Post
                        key={post.id}
                        post={post}
                        onPostChangeDelete={handlePostAddedRemovedChanged}
                    />
                ))}
            </Box>
        </Box>
    );
};

export default PostList;
