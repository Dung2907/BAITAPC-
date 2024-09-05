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
  DateInput,
} from "react-admin";

// Danh sách danh mục
export const CategoryList = () => (
  <List>
    <Datagrid>
      <TextField source="id" label="ID" />
      <TextField source="title" label="Title" />
      <TextField source="slug" label="Slug" />
      <TextField source="summary" label="Summary" />
      <ImageField source="photo" label="Photo" />
      <TextField source="isParent" label="Is Parent" />
      <TextField source="parentId" label="Parent ID" />
      <TextField source="addedBy" label="Added By" />
      <TextField source="status" label="Status" />
      <EditButton />
      <DeleteButton />
    </Datagrid>
  </List>
);

// Tạo mới danh mục


// Chỉnh sửa danh mục
export const CategoryEdit = () => (
  <Edit>
    <SimpleForm>
      <TextInput source="id" label="ID" />
      <TextInput source="title" label="Title" />
      <TextInput source="slug" label="Slug" />
      <TextInput source="summary" label="Summary" />
      {/* ImageInput allows uploading new images */}
      <ImageInput source="photo" label="Photo" />
      {/* ImageField displays the current image */}
      <ImageField source="photo" label="Current Photo" />
      <TextInput source="isParent" label="Is Parent" />
      <TextInput source="parentId" label="Parent ID" />
      <TextInput source="addedBy" label="Added By" />
      <TextInput source="status" label="Status" />
    </SimpleForm>
  </Edit>
);
