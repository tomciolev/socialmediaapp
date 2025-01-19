import axios from "axios";
import { createContext, useContext, useEffect, useMemo, useState } from "react";
import { UserDto } from "./PostList";


interface IAuthContext {
    token: string | null;
    setToken: (token: string | null) => void;
    user: UserDto | null,
    setUser: (user: UserDto | null) => void;
}

const AuthContext = createContext<IAuthContext>({
    token: null,
    setToken: () => { },
    user: null,
    setUser: () => { }
});

const AuthProvider = ({ children }: any) => {
    const [token, _setToken] = useState(localStorage.getItem("token"));
    const [user, _setUser] = useState<UserDto | null>(() => {
        const storedUser = localStorage.getItem("user");
        return storedUser ? JSON.parse(storedUser) : null;
    });

    const setToken = (newToken: string | null) => {
        _setToken(newToken);
    };
    const setUser = (newUser: UserDto | null) => {
        _setUser(newUser);
    };

    useEffect(() => {
        if (token) {
            axios.defaults.headers.common["Authorization"] = "Bearer " + token;
            localStorage.setItem('token', token);
        } else {
            delete axios.defaults.headers.common["Authorization"];
            localStorage.removeItem('token')
        }
    }, [token]);

    useEffect(() => {
        if (user) {
            localStorage.setItem('user', JSON.stringify(user));
        } else {
            localStorage.removeItem('user')
        }
    }, [user]);

    const contextValue = useMemo(
        () => ({
            token,
            setToken,
            user,
            setUser
        }),
        [token, user]
    );

    return (
        <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
};

export default AuthProvider;