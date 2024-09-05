// src/layout/Register.tsx
import React, { useState } from 'react';
import { TextField, Button, Typography, Paper } from '@mui/material';
import { authProvider } from '../authProvider'; // Import authProvider

const Register = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
   
    const [error, setError] = useState('');

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        if (password !== confirmPassword) {
            setError('Passwords do not match');
            return;
        }

        try {
            await authProvider.register({
                username,
                password,
                
            });
            // Redirect or show success message
            window.location.href = '/login';
        } catch (error) {
            setError('Registration failed');
        }
    };

    return (
        <Paper style={{ padding: '20px', maxWidth: '400px', margin: 'auto' }}>
            <Typography variant="h6">Register</Typography>
            <form onSubmit={handleSubmit}>
                <TextField
                    label="Username"
                    fullWidth
                    margin="normal"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
                
                <TextField
                    label="Password"
                    type="password"
                    fullWidth
                    margin="normal"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <TextField
                    label="Confirm Password"
                    type="password"
                    fullWidth
                    margin="normal"
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                />
                {error && <Typography color="error">{error}</Typography>}
                <Button type="submit" variant="contained" color="primary">
                    Register
                </Button>
            </form>
        </Paper>
    );
};

export default Register;
