import React, { useState } from "react";
import {
    Box,
    Card,
    CardContent,
    Typography,
    CardMedia,
    IconButton,
    Stack,
    CardHeader,
    Avatar,
    Menu,
    MenuItem,
    TextField,
    Button,
} from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import FavoriteIcon from "@mui/icons-material/Favorite";
import EmojiEmotionsIcon from "@mui/icons-material/EmojiEmotions";
import SentimentVeryDissatisfiedIcon from "@mui/icons-material/SentimentVeryDissatisfied";
import EmojiEventsIcon from "@mui/icons-material/EmojiEvents";
import axios from "axios";
import { PostProps } from "./PostList";
import { useAuth } from "./AuthProvider";

export function formatDate(dateString: string) {
    const datetimeArray = dateString.split("T");
    const date = datetimeArray[0];
    const time = datetimeArray[1].slice(0, 5);
    return date + " " + time;
}

const Post = ({ post, onPostChangeDelete }: { post: PostProps, onPostChangeDelete: () => void }) => {
    const { user } = useAuth();
    const [postReactions, setPostReactions] = useState(post.reactions);
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const [isEditing, setIsEditing] = useState(false);
    const [editedPost, setEditedPost] = useState(post);

    const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleEditPost = () => {
        setAnchorEl(null)
        setIsEditing(true);
    };

    const handleEditSubmit = async () => {
        try {
            await axios.put(`http://localhost:5073/post/${post.id}`, editedPost);
            setAnchorEl(null)
            setIsEditing(false)
            onPostChangeDelete();
        } catch (error) {
            console.error("Error while deleting post:", error);
        }
    }

    const handleDeletePost = async () => {
        try {
            await axios.delete(`http://localhost:5073/post/${post.id}`);
            console.log("Post usunięty:", post.id);
            setAnchorEl(null)
            onPostChangeDelete();
        } catch (error) {
            console.error("Error while deleting post:", error);
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setEditedPost((prev) => ({ ...prev, [name]: value }));
    };

    const handleReaction = async (reactionType: string) => {
        if (!user) return;

        const existingReaction = postReactions.find(
            (reaction) => reaction.emoji === reactionType && reaction.user.id === user.id
        );

        if (existingReaction) {
            try {
                await axios.delete(`http://localhost:5231/reaction/${existingReaction.id}`);
                setPostReactions((prev) =>
                    prev.filter((reaction) => reaction.id !== existingReaction.id)
                );
            } catch (error) {
                console.error("Error while removing reaction:", error);
            }
        } else {
            try {
                const response = await axios.post(`http://localhost:5231/reaction/${post.id}`, {
                    emoji: reactionType,
                    userId: user.id,
                });
                setPostReactions((prev) => [...prev, response.data]);
            } catch (error) {
                console.error("Error while adding reaction:", error);
            }
        }
    };

    const countReactions = (reactionType: string) =>
        postReactions.filter((reaction) => reaction.emoji === reactionType).length;

    return (
        <Card sx={{ maxWidth: 600, margin: "16px auto" }}>
            <CardHeader
                avatar={
                    <Avatar sx={{ bgcolor: "red" }} aria-label="recipe">
                        {post.user.firstName[0].toUpperCase()}
                    </Avatar>
                }
                title={`${post.user.firstName} ${post.user.lastName}`}
                subheader={formatDate(post.createdAt)}
                action={
                    user?.id === post.user.id && (
                        <IconButton onClick={handleMenuOpen}>
                            <MoreVertIcon />
                        </IconButton>
                    )
                }
            />
            <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={() => setAnchorEl(null)}
            >
                <MenuItem onClick={handleEditPost}>Edytuj</MenuItem>
                <MenuItem onClick={handleDeletePost}>Usuń</MenuItem>
            </Menu>
            {isEditing ? (
                <TextField
                    label="Url zdjęcia"
                    variant="outlined"
                    name="imageUrl"
                    value={editedPost.imageUrl}
                    onChange={handleInputChange}
                    required
                    fullWidth
                    sx={{ marginBottom: 2 }}
                />
            ) : (
                <CardMedia component="img" height="300" src={post.imageUrl} alt={post.title} />
            )}
            <CardContent>
                {isEditing ? (
                    <TextField
                        label="Tytuł"
                        variant="outlined"
                        name="title"
                        value={editedPost.title}
                        onChange={handleInputChange}
                        required
                        fullWidth
                        sx={{ marginBottom: 2 }}
                    />
                ) : (
                    <Typography variant="h5" component="div">
                        {post.title}
                    </Typography>
                )}
                {isEditing ? (
                    <TextField
                        label="Opis"
                        variant="outlined"
                        name="content"
                        value={editedPost.content}
                        onChange={handleInputChange}
                        required
                        fullWidth
                        sx={{ marginBottom: 2 }}
                    />
                ) : (
                    <Typography variant="body1" component="div">
                        {post.content}
                    </Typography>
                )}

                {isEditing ? (
                    <Button onClick={handleEditSubmit}>Zapisz zmiany</Button>
                ) : null}
            </CardContent>
            <Box sx={{ padding: "8px 16px" }}>
                <Stack direction="row" spacing={2}>
                    <IconButton aria-label="like" onClick={() => handleReaction("like")}>
                        <FavoriteIcon color="error" />
                        <Typography variant="body2" sx={{ marginLeft: "4px" }}>
                            {countReactions("like")}
                        </Typography>
                    </IconButton>
                    <IconButton aria-label="laugh" onClick={() => handleReaction("laugh")}>
                        <EmojiEmotionsIcon color="warning" />
                        <Typography variant="body2" sx={{ marginLeft: "4px" }}>
                            {countReactions("laugh")}
                        </Typography>
                    </IconButton>
                    <IconButton aria-label="congrats" onClick={() => handleReaction("congrats")}>
                        <EmojiEventsIcon color="warning" />
                        <Typography variant="body2" sx={{ marginLeft: "4px" }}>
                            {countReactions("congrats")}
                        </Typography>
                    </IconButton>
                    <IconButton aria-label="sad" onClick={() => handleReaction("sad")}>
                        <SentimentVeryDissatisfiedIcon color="warning" />
                        <Typography variant="body2" sx={{ marginLeft: "4px" }}>
                            {countReactions("sad")}
                        </Typography>
                    </IconButton>
                </Stack>
            </Box>
        </Card>
    );
};

export default Post;
