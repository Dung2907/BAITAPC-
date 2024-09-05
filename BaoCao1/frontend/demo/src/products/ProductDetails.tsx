import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from '../axios';
import { Typography, Container, Card, CardContent, CardMedia, CircularProgress } from '@mui/material';

interface Product {
    productId: number;
    photo?: string;
    title: string;
    price: number;
    discount: number;
    stock: number;
    cat?: { title: string };
    createdAt: string;
    updatedAt: string;
}

const ProductDetails = () => {
    const { productId } = useParams<{ productId: string }>();
    const [product, setProduct] = useState<Product | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchProduct = async () => {
            try {
                const response = await axios.get<Product>(`/Product/Get/${productId}`);
                setProduct(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Failed to fetch product data:', error);
                setLoading(false);
            }
        };

        fetchProduct();
    }, [productId]);

    if (loading) {
        return <CircularProgress />;
    }

    if (!product) {
        return <Typography variant="h6">Product not found.</Typography>;
    }

    return (
        <Container>
            <Card>
                {product.photo && (
                    <CardMedia
                        component="img"
                        alt={product.title}
                        height="300"
                        image={product.photo}
                        title={product.title}
                        style={{ objectFit: 'contain' }}
                    />
                )}
                <CardContent style={{ textAlign: 'center' }}>
                    <Typography variant="h4" component="h2">
                        Title: {product.title}
                    </Typography>
                    <Typography variant="h6" color="textSecondary">
                        Category: {product.cat?.title || 'N/A'}
                    </Typography>
                    <Typography variant="body1" component="p" gutterBottom>
                        Price: ${product.price}
                    </Typography>
                    {product.discount > 0 && (
                        <Typography variant="body2" component="p" color="secondary">
                            Discount: {product.discount}%
                        </Typography>
                    )}
                    <Typography variant="h6" component="p" color="primary">
                        Stock: {product.stock} units
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Created At: {new Date(product.createdAt).toLocaleDateString()}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Updated At: {new Date(product.updatedAt).toLocaleDateString()}
                    </Typography>
                </CardContent>
            </Card>
        </Container>
    );
};

export default ProductDetails;
