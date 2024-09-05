// src/components/ImageUpload.tsx

import React, { useState } from "react";
import axios from "axios";

interface ImageUploadProps {
    onImageUploaded: (url: string) => void;
}

const ImageUpload: React.FC<ImageUploadProps> = ({ onImageUploaded }) => {
    const [file, setFile] = useState<File | null>(null);
    const [uploading, setUploading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            setFile(file);
        }
    };

    const handleUpload = async () => {
        if (!file) return;

        const formData = new FormData();
        formData.append('file', file);

        try {
            setUploading(true);
            setError(null);

            const token = localStorage.getItem('jwt-token');
            if (!token) {
                throw new Error("Authentication token is missing.");
            }

            const response = await axios.post('http://localhost:5129/api/upload', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                    Authorization: `Bearer ${token}`,
                },
                withCredentials: true,
            });

            setUploading(false);
            onImageUploaded(response.data.FilePath);
        } catch (err) {
            setUploading(false);
            setError("Failed to upload image.");
            console.error(err);
        }
    };

    return (
        <div>
            <input type="file" onChange={handleFileChange} />
            <button onClick={handleUpload} disabled={uploading}>
                {uploading ? "Uploading..." : "Upload Image"}
            </button>
            {error && <p>{error}</p>}
        </div>
    );
};

export default ImageUpload;
