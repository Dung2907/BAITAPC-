import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from '../axios';
import { Typography, Container, Card, CardContent, CardMedia, CircularProgress } from '@mui/material';
interface Category {
    categoryId: number;
    title: string;
    slug: string;
    summary: string;
    photo: string;
    isParent: boolean;
    parentId: number | null;
    addedBy: string | null;
    status: string;
    createdAt: string;
    updatedAt: string;
}

const CategoryDetails = () => {
    const { categoryId } = useParams<{ categoryId: string }>();
    const [category, setCategory] = useState<Category | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchCategory = async () => {
            try {
                const response = await axios.get<Category>(`/Category/Get/${categoryId}`);
                setCategory(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Failed to fetch category data:', error);
                setLoading(false);
            }
        };

        fetchCategory();
    }, [categoryId]);

    if (loading) {
        return <CircularProgress />;
    }

    if (!category) {
        return <Typography variant="h6">Category not found.</Typography>;
    }

    return (
        <Container>
            <Card>
                {category.photo && (
                    <CardMedia
                        component="img"
                        alt={category.title}
                        height="300"
                        image={category.photo}
                        title={category.title}
                        style={{ objectFit: 'contain' }}
                    />
                )}
                <CardContent style={{ textAlign: 'center' }}>
                    <Typography variant="h4" component="h2">
                        Title: {category.title}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Slug: {category.slug}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Summary: {category.summary}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Is Parent: {category.isParent ? 'Yes' : 'No'}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Parent ID: {category.parentId ?? 'None'}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Added By: {category.addedBy ?? 'Unknown'}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Status: {category.status}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Created At: {new Date(category.createdAt).toLocaleDateString()}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                        Updated At: {new Date(category.updatedAt).toLocaleDateString()}
                    </Typography>
                </CardContent>
            </Card>
        </Container>
    );
};

export default CategoryDetails;
