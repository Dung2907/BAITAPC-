// src/authProvider.ts
import { AuthProvider } from "react-admin";
import axios from "./axios"; // Import axios instance

interface LoginParams {
    username: string;
    password: string;
}

interface RegisterParams {
    username: string;
    password: string;
    
}

interface CheckParamsErr {
    status: number;
}

export const authProvider: AuthProvider = {
    // called when the user attempts to log in
    login: async ({ username, password }: LoginParams) => {
        try {
            const response = await axios.post('/Auth/login', {
                username,
                password,
            });

            console.log('API Response:', response.data);

            const token = response.data as string; // Giả sử phản hồi là một chuỗi
            console.log('Extracted Token:', token);

            if (token) {
                localStorage.setItem("token", token);
                localStorage.setItem("username", username);
                // Trong file authProvider.ts
                console.log('Token from localStorage:', localStorage.getItem("token"));
                return Promise.resolve();
            } else {
                return Promise.reject(new Error("Token không có trong phản hồi"));
            }
        } catch (error) {
            return Promise.reject(new Error("Sai tài khoản hoặc mật khẩu. Vui lòng thử lại."));
        }
    },

    // called when the user attempts to register
    register: async ({ username, password }: RegisterParams) => {
        try {
            const response = await axios.post('/Auth/register', {
                username,
                password,
                
            });

            console.log('API Response:', response.data);

            // Bạn có thể cần thêm xử lý tùy thuộc vào phản hồi từ API

            return Promise.resolve();
        } catch (error) {
            return Promise.reject(new Error("Đăng ký thất bại. Vui lòng thử lại."));
        }
    },

    // called when the user clicks on the logout button
    logout: () => {
        localStorage.removeItem("token");
        localStorage.removeItem("username");
        return Promise.resolve();
    },

    // called when the API returns an error
    checkError: ({ status }: CheckParamsErr) => {
        if (status === 401 || status === 403) {
            localStorage.removeItem("token");
            localStorage.removeItem("username");
            return Promise.reject();
        }
        return Promise.resolve();
    },

    // called when the user navigates to a new location, to check for authentication
    checkAuth: () => {
        return localStorage.getItem("token") ? Promise.resolve() : Promise.reject();
    },

    // called when the user navigates to a new location, to check for permissions / roles
    getPermissions: () => {
        // Trả về quyền của người dùng, ví dụ:
        return Promise.resolve();  // Hoặc bạn có thể tùy chỉnh để trả về các quyền thực tế của người dùng
    },
};
