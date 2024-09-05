import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Grid, Button, TextField, MenuItem } from '@mui/material';
import axios from '../axios';

interface ProductValues {
    title: string;
    slug: string;
    summary: string;
    description: string;
    photo: string;
    stock: number;
    size: string;
    condition: string;
    status: string;
    price: number;
    discount: number;
    isFeatured: number;
    catId: number;
    childCatId: number;
    brandId: number;
    createdAt: string;
    updatedAt: string;
    productId?: number; // Nếu có trường `productId` có thể là tùy chọn
}

const ProductCreate = () => {
    const [values, setValues] = useState<ProductValues>({
        title: '',
        slug: '',
        summary: '',
        description: '',
        photo: '',
        stock: 0,
        size: '',
        condition: '',
        status: '',
        price: 0,
        discount: 0,
        isFeatured: 0,
        catId: 0,
        childCatId: 0,
        brandId: 0,
        createdAt: new Date().toISOString().split('T')[0],
        updatedAt: new Date().toISOString().split('T')[0],
    });

    const [categories, setCategories] = useState<any[]>([]);
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();

    useEffect(() => {
        axios.get('/Category/GetAll')
            .then(response => {
                console.log('Categories:', response.data);
                setCategories(response.data);
            })
            .catch(error => console.error('Error fetching categories:', error));
    }, []);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        console.log(`Name: ${name}, Value: ${value}`);
        setValues(prevValues => ({
            ...prevValues,
            [name]: name === 'catId' ? parseInt(value, 10) : value
        }));
    };

    const handleFileChange: React.ChangeEventHandler<HTMLInputElement> = (e) => {
        if (e.target.files && e.target.files[0]) {
            const file = e.target.files[0];
            const reader = new FileReader();
            reader.onloadend = () => {
                setValues(prevValues => ({
                    ...prevValues,
                    photo: reader.result as string
                }));
            };
            reader.readAsDataURL(file);
        }
    };

    const validate = () => {
        let tempErrors: any = {};
        if (!values.title) tempErrors.title = 'Title is required.';
        if (values.price <= 0) tempErrors.price = 'Price must be greater than 0.';
        if (values.stock < 0) tempErrors.stock = 'Stock cannot be negative.';
        if (values.catId === 0) tempErrors.catId = 'Category is required.';

        setErrors(tempErrors);
        return Object.keys(tempErrors).length === 0;
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (validate()) {
            axios.post('/Product/Add', values, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => {
                    if (response.data.success) {
                        navigate('/products', { replace: true });
                    } else {
                        console.error('API Error:', response.data.message);
                    }
                })
                .catch(error => console.error('Error:', error));
        }
    };

    const handleReset = () => {
        setValues({
            title: '',
            slug: '',
            summary: '',
            description: '',
            photo: '',
            stock: 0,
            size: '',
            condition: '',
            status: '',
            price: 0,
            discount: 0,
            isFeatured: 0,
            catId: 0,
            childCatId: 0,
            brandId: 0,
            createdAt: new Date().toISOString().split('T')[0],
            updatedAt: new Date().toISOString().split('T')[0],
        });
        setErrors({});
    };
    const navigateToProductList = () => {
        navigate('/products');

    };
    return (
        <Grid container justifyContent="center" alignItems="center" direction="column" style={{ minHeight: '100vh' }}>
            <h1>{values.productId ? 'Edit Product' : 'Add Product'}</h1>
            <form onSubmit={handleSubmit} style={{ width: 600 }}>
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextField
                            id="title"
                            name="title"
                            label="Title"
                            value={values.title}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            id="slug"
                            name="slug"
                            label="Slug"
                            value={values.slug}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            id="summary"
                            name="summary"
                            label="Summary"
                            value={values.summary}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            id="description"
                            name="description"
                            label="Description"
                            value={values.description}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={12}>
                        {values.photo && (
                            <img src={values.photo} alt="photo" style={{ width: '100px', height: '100px', marginBottom: '10px' }} />
                        )}
                        <input
                            type="file"
                            id="photo"
                            name="photo"
                            accept="image/*"
                            onChange={handleFileChange}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            id="price"
                            name="price"
                            label="Price"
                            type="number"
                            value={values.price}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            id="discount"
                            name="discount"
                            label="Discount"
                            type="number"
                            value={values.discount}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            id="stock"
                            name="stock"
                            label="Stock"
                            type="number"
                            value={values.stock}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            id="size"
                            name="size"
                            label="Size"
                            value={values.size}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            id="condition"
                            name="condition"
                            label="Condition"
                            value={values.condition}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            id="status"
                            name="status"
                            label="Status"
                            value={values.status}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            id="catId"
                            name="catId"
                            label="Category"
                            select
                            value={values.catId}
                            onChange={handleChange}
                            fullWidth
                        >
                            {categories.map((cat) => (
                                <MenuItem key={cat.categoryId} value={cat.categoryId}>
                                    {cat.title}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item xs={12}>
                        <Button type="submit" variant="contained" color="primary">
                            {values.productId ? 'Update' : 'Add'}
                        </Button>
                        <Button type="button" onClick={handleReset} variant="outlined" color="secondary" style={{ marginLeft: '10px' }}>
                            Reset
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </Grid>
    );
};

export default ProductCreate;
