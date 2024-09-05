import axios from 'axios';
import { useNotify, useRedirect } from 'react-admin';
import { useState } from 'react';
import { SaveButton, SaveButtonProps } from 'react-admin';

const CustomSaveButton = (props: SaveButtonProps) => {
    const [loading, setLoading] = useState(false);
    const notify = useNotify();
    const redirect = useRedirect();

    const handleClick = async () => {
        setLoading(true);
        try {
            // Get form data here
            const formData = new FormData();
            // Append form data here

            // Submit form data to the API
            await axios.post('http://localhost:5129/api/products', formData);
            notify('Product created successfully!');
            redirect('/products');
        } catch (error: unknown) {
            // Check if error is an AxiosError
            if (axios.isAxiosError(error)) {
                // Handle Axios error
                notify(`Error creating product: ${error.response?.data?.message || error.message}`);
                console.error('API Error response data:', error.response?.data);
                console.error('API Error status code:', error.response?.status);
            } else {
                // Handle other errors
                notify(`Error creating product: ${error instanceof Error ? error.message : 'Unknown error'}`);
                console.error('Unexpected error:', error);
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <SaveButton {...props} onClick={handleClick} disabled={loading} />
    );
};

export default CustomSaveButton;
