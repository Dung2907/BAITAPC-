import React, { useState, useMemo, useEffect, Fragment } from "react";
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import { Container, Button, TextField, Box } from "@material-ui/core";
import { useNavigate } from "react-router-dom";
import moment from "moment-timezone";
import axios from '../axios';
import { CreateButton, ExportButton, SortButton, TopToolbar } from "react-admin";

// API functions
const fetchProductsFromAPI = async () => {
    try {
        const response = await axios.get('/Product/GetAll');
        return response.data;
    } catch (error) {
        console.error('Failed to fetch products', error);
        throw error;
    }
};

const deleteProductFromAPI = async (productId: number) => {
    try {
        await axios.delete(`/Product/Delete/${productId}`);
    } catch (error) {
        console.error('Failed to delete product', error);
        throw error;
    }
};

interface IProduct {
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

interface Column {
    id: 'photo' | 'title' | 'price' | 'discount' | 'stock' | 'category' | 'createdAt' | 'updatedAt' | 'edit' | 'delete' | 'view';
    label: string;
    minWidth?: number;
    align?: 'right';
    format?: (value: any) => JSX.Element;
}

const columns: Column[] = [
    {
        id: 'photo',
        label: 'Photo',
        minWidth: 100,
        format: (value: any) => (
            <img src={value} alt="Product" style={{ width: '50px', height: '50px', objectFit: 'cover' }} />
        ),
    },
    { id: 'title', label: 'Title', minWidth: 100 },
    { id: 'price', label: 'Price', minWidth: 100 },
    { id: 'discount', label: 'Discount', minWidth: 100 },
    { id: 'stock', label: 'Stock', minWidth: 100 },
    { id: 'category', label: 'Category', minWidth: 100 },
    { id: 'createdAt', label: 'Created At', minWidth: 150 },
    { id: 'updatedAt', label: 'Updated At', minWidth: 150 },
    { id: 'edit', label: 'Edit', minWidth: 100 },
    { id: 'delete', label: 'Delete', minWidth: 100 },
    { id: 'view', label: 'View', minWidth: 100 },
];

const useStyles = makeStyles({
    root: {
        width: '100%',
    },
    container: {
        maxHeight: 440,
    },
    buttonContainer: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: '1rem',
    },
    buttonGroup: {
        display: 'flex',
        gap: '1rem',
    },
});

export const ProductList = () => {
    const classes = useStyles();
    const [products, setProducts] = useState<IProduct[]>([]);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [search, setSearch] = useState('');
    const navigate = useNavigate();
    const isLoggedIn = !!localStorage.getItem('token');

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const productList = await fetchProductsFromAPI();
                setProducts(productList);
            } catch (error) {
                console.error('Failed to fetch products', error);
            }
        };

        fetchProducts();
    }, []);

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };

    const filteredData = useMemo(() => {
        if (Array.isArray(products)) {
            return products.filter((x: IProduct) => !search || x.title.toLowerCase().includes(search.toLowerCase()));
        }
        return [];
    }, [products, search]);

    const navigateToAddProduct = () => {
        navigate("/add-product");
    };

    const navigateToEditProduct = (record: IProduct) => {
        navigate(`/products/edit/${record.productId}`);
    };

    const navigateToViewProduct = (record: IProduct) => {
        navigate(`/products/detail/${record.productId}`);
    };

    const handleDeleteProduct = async (record: IProduct) => {
        try {
            await deleteProductFromAPI(record.productId);
            setProducts(products.filter(product => product.productId !== record.productId));
        } catch (error) {
            console.error('Failed to delete product', error);
        }
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate("/login");
    };

    return (
        <Fragment>

            <TopToolbar>
                <CreateButton />
            </TopToolbar>
            <h1 style={{ textAlign:'center'}}>Product Information</h1>
            <Container>
                <div style={{ display: 'flex', alignItems: 'center', gap: '16px', justifyContent: 'center' }}>
                    
                    <TextField
                        id="search"
                        label="Search"
                        variant="outlined"
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                        style={{ width: '330px', borderRadius: '8px', marginBottom: '0', boxSizing: 'content-box' }}
                    />
                </div>

                <div>&nbsp;</div>

                <Paper className={classes.root}>
                    <TableContainer className={classes.container}>
                        <Table stickyHeader aria-label="sticky table">
                            <TableHead>
                                <TableRow>
                                    {columns.map((column) => (
                                        <TableCell
                                            key={column.id}
                                            align={column.align}
                                            style={{ minWidth: column.minWidth }}
                                        >
                                            {column.label}
                                        </TableCell>
                                    ))}
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {Array.isArray(filteredData) ? filteredData.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((record: IProduct) => (
                                    <TableRow key={record.productId}>
                                        <TableCell>
                                            {record.photo ? (
                                                <img
                                                    src={record.photo}
                                                    alt="Product"
                                                    style={{ width: '50px', height: '50px', objectFit: 'cover' }}
                                                />
                                            ) : (
                                                'No Image'
                                            )}
                                        </TableCell>
                                        <TableCell>{record.title}</TableCell>
                                        <TableCell>{record.price}</TableCell>
                                        <TableCell>{record.discount}</TableCell>
                                        <TableCell>{record.stock}</TableCell>
                                        <TableCell>{record.cat?.title || 'Unknown'}</TableCell>
                                        <TableCell>
                                            {moment(new Date(record.createdAt || '')).format("MM/DD/YYYY")}
                                        </TableCell>
                                        <TableCell>
                                            {moment(new Date(record.updatedAt || '')).format("MM/DD/YYYY")}
                                        </TableCell>
                                        <TableCell>
                                            <Button
                                                onClick={() => navigateToEditProduct(record)}
                                                color="primary"
                                                size="small"
                                                variant="contained"
                                            >
                                                Edit
                                            </Button>
                                        </TableCell>
                                        <TableCell>
                                            <Button
                                                onClick={() => handleDeleteProduct(record)}
                                                color="secondary"
                                                size="small"
                                                variant="contained"
                                            >
                                                Delete
                                            </Button>
                                        </TableCell>
                                        <TableCell>
                                            <Button
                                                onClick={() => navigateToViewProduct(record)}
                                                color="primary"
                                                size="small"
                                                variant="contained"
                                            >
                                                View
                                            </Button>
                                        </TableCell>
                                    </TableRow>
                                )) : (
                                    <TableRow>
                                        <TableCell colSpan={columns.length}>No data available</TableCell>
                                    </TableRow>
                                )}
                            </TableBody>
                        </Table>
                    </TableContainer>
                    <TablePagination
                        rowsPerPageOptions={[10, 25, 100]}
                        component="div"
                        count={filteredData.length}
                        rowsPerPage={rowsPerPage}
                        page={page}
                        onPageChange={handleChangePage}
                        onRowsPerPageChange={handleChangeRowsPerPage}
                    />
                </Paper>
            </Container>

        </Fragment>
    );
};
