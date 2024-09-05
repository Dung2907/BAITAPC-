import React, { useState, useMemo, useEffect } from "react";
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import { Container, Button, TextField } from "@material-ui/core";

import { useNavigate } from "react-router-dom";
import moment from "moment-timezone";

import axios from "../axios";
import { CreateButton, TopToolbar } from "react-admin";

// Định nghĩa interface cho cột
interface Column {
  id: string;
  label: string;
  minWidth?: number;
  align?: 'right' | 'left' | 'center'; // Thêm thuộc tính align
  format?: (value: any) => JSX.Element;
}

const columns: Column[] = [
  {
    id: 'photo',
    label: 'Photo',
    minWidth: 100,
    format: (value: any) => (
      <img src={value} alt="Category" style={{ width: '50px', height: '50px', objectFit: 'cover' }} />
    ),
  },
  { id: 'title', label: 'Title', minWidth: 100 },
  { id: 'slug', label: 'Slug', minWidth: 100 },
  { id: 'summary', label: 'Summary', minWidth: 150 },
  { id: 'status', label: 'Status', minWidth: 100 },
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

export const CategoryList = () => {
  const classes = useStyles();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const [search, setSearch] = useState('');
  const [categoryRecords, setCategoryRecords] = useState<any[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  const isLoggedIn = !!localStorage.getItem('token');

  useEffect(() => {
    const fetchCategories = async () => {
      setLoading(true);
      try {
        const response = await axios.get('/Category/GetAll'); // URL thực tế của API
        setCategoryRecords(response.data);
      } catch (error) {
        setError("Failed to fetch categories.");
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, []);

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const filteredData = useMemo(() => {
    return categoryRecords.filter((x: any) => !search || x.title.toLowerCase().includes(search.toLowerCase()));
  }, [categoryRecords, search]);

  const navigateToAddCategory = () => {
    navigate("/categories/create");
  };

  const navigateToEditCategory = (record: any) => {
    navigate(`/categories/edit/${record.categoryId}`);
  };

  const navigateToViewCategory = (record: any) => {
    navigate(`/categories/detail/${record.categoryId}`);
  };

  const deleteCategory = async (record: any) => {
    try {
      await axios.delete(`/Category/Delete/${record.categoryId}`);
      setCategoryRecords((prev) => prev.filter((x) => x.categoryId !== record.categoryId));
    } catch (error) {
      setError("Failed to delete category.");
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate("/login");
  };

  return (
    <>
    <TopToolbar>
        <CreateButton />
    </TopToolbar>
    <h1 style={{ textAlign:'center'}}>Category Information</h1>
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
                {filteredData.length > 0 ? filteredData.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((record: any) => (
                  <TableRow key={record.categoryId}>
                    <TableCell>
                      {record.photo ? (
                        <img
                          src={record.photo}
                          alt="Category"
                          style={{ width: '50px', height: '50px', objectFit: 'cover' }}
                        />
                      ) : (
                        'No Image'
                      )}
                    </TableCell>
                    <TableCell>{record.title}</TableCell>
                    <TableCell>{record.slug}</TableCell>
                    <TableCell>{record.summary}</TableCell>
                    <TableCell>{record.status}</TableCell>
                    <TableCell>
                      {moment(new Date(record.createdAt)).format("MM/DD/YYYY")}
                    </TableCell>
                    <TableCell>
                      {moment(new Date(record.updatedAt)).format("MM/DD/YYYY")}
                    </TableCell>
                    <TableCell>
                      <Button
                        color="primary"
                        size="small"
                        variant="contained"
                        onClick={() => navigateToEditCategory(record)}
                      >
                        Edit
                      </Button>
                    </TableCell>
                    <TableCell>
                      <Button
                        color="secondary"
                        size="small"
                        variant="contained"
                        onClick={() => deleteCategory(record)}
                      >
                        Delete
                      </Button>
                    </TableCell>
                    <TableCell>
                      <Button
                        color="primary"
                        size="small"
                        variant="contained"
                        onClick={() => navigateToViewCategory(record)}
                      >
                        View
                      </Button>
                    </TableCell>
                  </TableRow>
                )) : (
                  <TableRow>
                    <TableCell colSpan={columns.length} align="center">
                      No records found
                    </TableCell>
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
    </>
  );
};
