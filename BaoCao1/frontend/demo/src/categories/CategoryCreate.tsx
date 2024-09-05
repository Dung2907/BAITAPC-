import React, { useState } from 'react';
import { makeStyles } from '@mui/styles';
import Grid from '@mui/material/Grid';
import { useNavigate } from 'react-router-dom';
import axios from '../axios';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

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

const initialState = {
    categoryId: 0,
    title: '',
    slug: '',
    summary: '',
    photo: '',
    isParent: '', // Hoặc false nếu bạn muốn sử dụng boolean
    parentId: null,
    addedBy: null,
    status: '',
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
};

export const CategoryCreate = () => {
    const classes = useStyles();
    const navigate = useNavigate();
    const [values, setValues] = useState(initialState);
    const [errors, setErrors] = useState<any>({});

    const validate = (fieldValues = values) => {
        let temp: any = { ...errors };
        if ('title' in fieldValues) {
            if (fieldValues.title === '') {
                temp.title = 'This field is required.';
            } else {
                temp.title = '';
            }
        }
        if ('slug' in fieldValues) {
            if (fieldValues.slug === '') {
                temp.slug = 'This field is required.';
            } else {
                temp.slug = '';
            }
        }
        // Thêm các kiểm tra khác nếu cần

        setErrors({
            ...temp,
        });

        if (fieldValues === values) {
            return Object.values(temp).every((x) => x === '');
        }
    };

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (validate()) {
            try {
                const response = await axios.post('/Category/Add', values);
                console.log('API Response:', response.data);
                if (response.status === 201) {
                    navigate('/categories');
                }
            } catch (error) {
                
                    console.error('API Error:', error);
                
            }
        } else {
            console.log('Form Validation Error');
        }
    };
    

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files[0]) {
            const file = event.target.files[0];
            const reader = new FileReader();
            reader.onloadend = () => {
                const base64String = reader.result as string;
                setValues({
                    ...values,
                    photo: base64String,
                });
            };
            reader.readAsDataURL(file);
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setValues({
            ...values,
            [name]: value,
        });
    };

    const resetFormDetails = () => {
        setValues(initialState);
        setErrors({});
    };

    const navigateToCategoryList = () => {
        navigate('/categories');
    };

    return (
        <>
            <Grid container justifyContent="center" alignItems="center" direction="column" style={{ minHeight: '100vh' }}>
                <h1>Create Category</h1>
                <form onSubmit={handleSubmit} style={{ width: 600 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField
                                id="title"
                                name="title"
                                label="Title"
                                value={values.title}
                                onChange={handleChange}
                                error={!!errors.title}
                                helperText={errors.title}
                                fullWidth
                                className={classes.field}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="slug"
                                name="slug"
                                label="Slug"
                                value={values.slug}
                                onChange={handleChange}
                                error={!!errors.slug}
                                helperText={errors.slug}
                                fullWidth
                                className={classes.field}
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
                                className={classes.field}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            {values.photo && (
                                <img
                                    src={values.photo}
                                    alt="Category"
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
                        <Grid item xs={12}>
                            <TextField
                                id="isParent"
                                name="isParent"
                                label="Is Parent"
                                type="number"
                                value={values.isParent}
                                onChange={handleChange}
                                fullWidth
                                className={classes.field}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="parentId"
                                name="parentId"
                                label="Parent ID"
                                type="number"
                                value={values.parentId}
                                onChange={handleChange}
                                fullWidth
                                className={classes.field}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="addedBy"
                                name="addedBy"
                                label="Added By"
                                type="number"
                                value={values.addedBy}
                                onChange={handleChange}
                                fullWidth
                                className={classes.field}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="status"
                                name="status"
                                label="Status"
                                value={values.status}
                                onChange={handleChange}
                                fullWidth
                                className={classes.field}
                            />
                        </Grid>
                    </Grid>

                    <Grid container spacing={2} justifyContent="flex-end" style={{ marginTop: 10 }}>
                        <Grid item>
                            <Button
                                variant="contained"
                                color="primary"
                                type="submit"
                                className={classes.button}
                            >
                                Submit
                            </Button>
                        </Grid>
                        <Grid item>
                            <Button
                                variant="contained"
                                onClick={resetFormDetails}
                                className={classes.button}
                            >
                                Reset
                            </Button>
                        </Grid>
                        <Grid item>
                            <Button
                                variant="contained"
                                onClick={navigateToCategoryList}
                                className={classes.button}
                            >
                                Cancel
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </Grid>
        </>
    );
};
