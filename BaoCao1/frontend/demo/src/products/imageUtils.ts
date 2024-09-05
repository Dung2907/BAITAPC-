// imageUtils.ts

// Handles uploading an image file and returns the URL of the uploaded file
export const handleImageUpload = async (file: File): Promise<string> => {
    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await fetch('http://localhost:5129/api/upload', {
            method: 'POST',
            body: formData,
        });

        if (!response.ok) {
            throw new Error('Image upload failed'); // Error handling if upload fails
        }

        const result = await response.json();
        return result.fileUrl; // Assume the server returns the file URL in the 'fileUrl' field
    } catch (error) {
        console.error('Image upload error:', error);
        throw error; // Re-throw error to be handled by the caller
    }
};

// Validates the image file based on size (and optionally other criteria)
export const validateImage = (file: File): boolean => {
    // Example: File size should be less than 5MB
    return file.size < 5000000; // 5MB in bytes
};
