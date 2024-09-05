import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Grid, Button, TextField, MenuItem } from '@mui/material';
import axios from '../axios';
import { makeStyles } from '@mui/styles';
const useStyles = makeStyles({
    field: {
        marginTop: 20,
        marginBottom: 20,
    },
    formContainer: {
        width: '100%',
        maxWidth: 400,
        padding: 20,
        backgroundColor: '#f5f5f5',
        borderRadius: 10,
    },
    formTitle: {
        marginBottom: 20,
    },
    formButton: {
        marginTop: 20,
    },
    button: {
        padding: '10px',
        borderRadius: '4px',
        border: 'none',
        backgroundColor: '#007bff',
        color: '#fff',
        fontSize: '16px',
        cursor: 'pointer',
        width: 100,
    },
});
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
}

const ProductEdit = () => {
    const { productId } = useParams<{ productId: string }>();
    console.log('Product ID:', productId);
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
        createdAt: '',
        updatedAt: '',
    });

    const [categories, setCategories] = useState<any[]>([]);
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();
    const classes = useStyles();
    useEffect(() => {
        // Lấy danh sách các category
        axios.get('/Category/GetAll')
            .then(response => {
                setCategories(response.data);
            })
            .catch(error => console.error('Error fetching categories:', error));

        // Nếu có productId, tải thông tin sản phẩm
        console.log('Product ID:', productId); // Kiểm tra giá trị productId
    if (productId) {
        axios.get(`/Product/Get/${productId}`)
            .then(response => {
                console.log('Product Data:', response.data); // Kiểm tra dữ liệu trả về
                setValues(response.data);
            })
            .catch(error => console.error('Error fetching product:', error));
    }
    }, [productId]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
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
    
        if (!productId) {
            alert('Product ID is missing. Cannot update product.');
            return;
        }
    
        if (validate()) {
            axios({
                method: 'put',
                url: `/Product/Update`,
                data: { ...values, id: productId }, // Đảm bảo productId được gửi kèm
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
    

    return (
        <Grid container justifyContent="center" alignItems="center" direction="column" style={{ minHeight: '100vh' }}>
            <h1>Edit Product</h1>
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
                            <img
                                src={values.photo}
                                alt="Product"
                                style={{
                                    width: '100px',
                                    height: '100px',
                                    marginBottom: '10px',
                                    display: 'block',
                                    marginLeft: '15px',
                                }}
                            />
                        )}
                        <input
                            type="file"
                            id="photo"
                            name="photo"
                            accept="image/*"
                            onChange={handleFileChange}
                            className={classes.field}
                            style={{ display: 'block', marginLeft: 20 }}
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
                            Update
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </Grid>
    );
};

export default ProductEdit;
