import {
  List,
  Datagrid,
  TextField,
  DeleteButton,
  EditButton,
  Edit,
  SimpleForm,
  TextInput,
  Create,
  ImageInput,
  ImageField,
  NumberInput,
  ReferenceInput,
  SelectInput,
  DateInput,
  required,
  useNotify,
  useRedirect,
  SaveButton,
  Toolbar,
  ToolbarProps,
  useDataProvider
} from "react-admin";
import ImageUpload from "./ImageUpload";
import { useState } from "react";
import axios from "axios";
import CustomSaveButton from "./CustomSaveButton";
// Danh sách danh mục
// export const ProductList = () => (
//   <List>
//     <Datagrid>
//       <TextField source="id" label="ID" />
//       <TextField source="catId" label="Cat ID" />
//       <TextField source="title" label="Title" />
//       <TextField source="description" label="Description" />
//       <TextField source="summary" label="Summary" />
//       <ImageField source="photo" label="Photo" />
//       <TextField source="size" label="Size" />
//       <TextField source="condition" label="Condition" />
//       <TextField source="isParent" label="Is Parent" />
//       <TextField source="price" label="Price" />
//       <TextField source="addedBy" label="Added By" />
//       <TextField source="status" label="Status" />
//       <EditButton />
//       <DeleteButton />
//     </Datagrid>
//   </List>
// );
// export const ProductCreate = () => {
//   const [base64Photo, setBase64Photo] = useState<string | null>(null);
//   const notify = useNotify();
//   const redirect = useRedirect();

//   const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
//     const file = event.target.files && event.target.files[0];
//     if (file) {
//       const reader = new FileReader();
//       reader.readAsDataURL(file);
//       reader.onload = () => {
//         const base64String = reader.result as string;
//         setBase64Photo(base64String);
//         console.log(base64String); // Logs Base64 string of the image
//       };
//       reader.onerror = (error) => {
//         console.error('Error reading file: ', error);
//       };
//     }
//   };

//   const handleSuccess = (data: any) => {
//     // Handle success response if needed
//     console.log('Form submitted successfully:', data);
//     notify('Product created successfully!');
//     redirect('/products'); // Redirect after successful creation
//   };

//   const handleFailure = (error: any) => {
//     // Handle failure response if needed
//     console.error('Form submission failed:', error);
//     notify('Error creating product!');
//   };

//   const handleSubmit = async (data: any) => {
//     // Add the Base64 photo data to the form data
//     const formData = {
//       ...data,
//       Photo: base64Photo
//     };

//     try {
//       // Here you can handle the form submission, e.g., using fetch or axios
//       const response = await fetch('/api/products', {
//         method: 'POST',
//         headers: {
//           'Content-Type': 'application/json',
//         },
//         body: JSON.stringify(formData),
//       });
      
//       const result = await response.json();

//       // Handle success or failure based on the response
//       if (response.ok) {
//         handleSuccess(result);
//       } else {
//         handleFailure(result);
//       }
//     } catch (error) {
//       handleFailure(error);
//     }
//   };

//   return (
//     <Create>
//       <SimpleForm save={handleSubmit}>
//         <TextInput source="title" label="Title" validate={required()} defaultValue="Máy ảnh" />
//         <TextInput source="slug" label="Slug" validate={required()} defaultValue="may-anh" />
//         <TextInput source="summary" label="Summary" validate={required()} defaultValue="tai nghe cuc hay" />
//         <TextInput source="description" label="Description" defaultValue="Microsoft Surface Pro 11" />
//         <NumberInput source="stock" label="Stock" defaultValue={3} />
//         <TextInput source="size" label="Size" defaultValue="big" />
//         <TextInput source="condition" label="Condition" defaultValue="new" />
//         <TextInput source="status" label="Status" defaultValue="1" />
//         <NumberInput source="price" label="Price" defaultValue={600} />
//         <NumberInput source="discount" label="Discount" defaultValue={0} />
//         <NumberInput source="isFeatured" label="Is Featured" defaultValue={1} />

//         <ReferenceInput source="catId" reference="Categories" label="Category" defaultValue={1}>
//           <SelectInput optionText="title" />
//         </ReferenceInput>

//         {/* Image input for photo */}
//         <div>
//           <label htmlFor="photo">Photo:</label>
//           <input type="file" id="photo" onChange={handleImageChange} />
//           {base64Photo && (
//             <img src={base64Photo} alt="Uploaded Preview" style={{ maxWidth: '200px', marginTop: '10px' }} />
//           )}
//         </div>

//         <DateInput source="createdAt" label="Created At" defaultValue="2024-09-10T00:00:00" />
//       </SimpleForm>
//     </Create>
//   );
// };
// export const ProductCreate = () => {
//   const [base64Photo, setBase64Photo] = useState<string | null>(null);

//   const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
//     const file = event.target.files && event.target.files[0];
//     if (file) {
//       const reader = new FileReader();
//       reader.readAsDataURL(file);
//       reader.onload = () => {
//         setBase64Photo(reader.result as string);
//         console.log(reader.result); // This will print the Base64 string
//       };
//       reader.onerror = (error) => {
//         console.error('Error: ', error);
//       };
//     }
//   };

//   return (
//     <Create>
//       <SimpleForm>
//         <TextInput source="title" label="Title" validate={required()} defaultValue="Máy ảnh" />
//         <TextInput source="slug" label="Slug" validate={required()} defaultValue="may-anh" />
//         <TextInput source="summary" label="Summary" validate={required()} defaultValue="tai nghe cuc hay" />
//         <TextInput source="description" label="Description" defaultValue="Microsoft Surface Pro 11" />
//         <NumberInput source="stock" label="Stock" defaultValue={3} />
//         <TextInput source="size" label="Size" defaultValue="big" />
//         <TextInput source="condition" label="Condition" defaultValue="new" />
//         <TextInput source="status" label="Status" defaultValue="1" />
//         <NumberInput source="price" label="Price" defaultValue={600} />
//         <NumberInput source="discount" label="Discount" defaultValue={0} />
//         <NumberInput source="isFeatured" label="Is Featured" defaultValue={1} />

//         <ReferenceInput source="catId" reference="Categories" label="Category" defaultValue={1}>
//           <SelectInput optionText="title" />
//         </ReferenceInput>

//         {/* Image input for photo */}
//         <div>
//           <label htmlFor="photo">Photo:</label>
//           <input type="file" id="photo" onChange={handleImageChange} />
//           {base64Photo && (
//             <img src={base64Photo} alt="Uploaded Preview" style={{ maxWidth: '200px', marginTop: '10px' }} />
//           )}
//         </div>
//         <DateInput source="createdAt" label="Created At" defaultValue="2024-09-10T00:00:00" />
//       </SimpleForm>
//     </Create>
//   );
// };
// Chỉnh sửa danh mục
export const ProductEdit = () => (
  <Edit>
    <SimpleForm>
      <TextInput source="title" label="Title" />
      <TextInput source="slug" label="Slug" />
      <TextInput source="summary" label="Summary" />
      <TextInput source="description" label="Description" />
      <NumberInput source="stock" label="Stock" />
      <TextInput source="size" label="Size" />
      <TextInput source="condition" label="Condition" />
      <TextInput source="status" label="Status" />
      <NumberInput source="price" label="Price" />
      <NumberInput source="discount" label="Discount" />
      <NumberInput source="isFeatured" label="Is Featured" />

      {/* Fetch and display categories */}
      <ReferenceInput source="catId" reference="Categories" label="Category">
        <SelectInput optionText="title" />
      </ReferenceInput>

      {/* Image input for photo */}
      <ImageInput source="photo" label="Photo" />
      <ImageField source="photo" label="Photo" />

      {/* Optionally handle creation date and updated date if applicable */}
      <DateInput source="createdAt" label="Created At" />
    </SimpleForm>
  </Edit>
);
