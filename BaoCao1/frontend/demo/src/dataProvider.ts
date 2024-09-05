// import axios from "axios";
// import {
//   CreateParams,
//   DataProvider,
//   DeleteManyParams,
//   DeleteParams,
//   GetManyParams,
//   GetOneParams,
//   RaRecord,
//   UpdateParams,
// } from "react-admin";
// interface PostResponse<T = any> {
//   json: T;
// }
// const apiUrl = "http://localhost:5129/api";

// const getToken = () => {
//   const token = localStorage.getItem("token");
//   if (!token) {
//     console.warn("Token not found");
//     throw new Error("Authentication token is missing.");
//   }
//   return token;
// };

// const httpClient = {
//   get: async (url: string) => {
//     const token = localStorage.getItem("token");
//     try {
//       const response = await axios.get(url, {
//         headers: {
//           Authorization: `Bearer ${token}`,
//           "Content-Type": "application/json",
//         },
//         withCredentials: true,
//       });
//       return { json: response.data };
//     } catch (error) {
//       console.error("API request failed:", error);
//       throw error;
//     }
//   },
//   // post: async (url: string, data: any) => {
//   //   const token = localStorage.getItem("jwt-token");
//   //   try {
//   //     const response = await axios.post(url, data, {
//   //       headers: {
//   //         Authorization: `Bearer ${token}`,
//   //         "Content-Type": "application/json",
//   //       },
//   //       withCredentials: true,
//   //     });
//   //     return { json: response.data };
//   //   } catch (error) {
//   //     console.error("API request failed:", error);
//   //     throw error;
//   //   }
//   // },
//   post: async (url: string, data: any, isFileUpload: boolean = false) => {
//     const token = getToken();
//     try {
//       console.log('Sending POST request to:', url);

//       const headers = {
//         Authorization: `Bearer ${token}`,
//         'Content-Type': isFileUpload ? 'multipart/form-data' : 'application/json',
//       };

//       if (isFileUpload) {
//         if (!(data instanceof FormData)) {
//           console.error('Expected FormData for file upload.');
//           throw new Error('Invalid data format for file upload.');
//         }
//       } else {
//         data = JSON.stringify(data);
//       }

//       const response = await axios.post(url, data, {
//         headers,
//         withCredentials: true,
//       });

//       console.log('API response data:', response.data);
//       return { json: response.data };
//     } catch (error: any) {
//       if (axios.isAxiosError(error) && error.response) {
//         console.error('Error response data:', error.response.data);
//         console.error('Error status code:', error.response.status);
//         console.error('Error response headers:', error.response.headers);
//       } else {
//         console.error('API request failed:', error);
//       }
      
//       throw new Error('Failed to complete the request. Please check your input data and try again.');
//     }
//   },
  
//   put: async (url: string, data: any) => {
//     const token = localStorage.getItem("token");
//     try {
//       const response = await axios.put(url, data, {
//         headers: {
//           Authorization: `Bearer ${token}`,
//           "Content-Type": "application/json",
//         },
//         withCredentials: true,
//       });
//       return { json: response.data };
//     } catch (error) {
//       console.error("API request failed:", error);
//       throw error;
//     }
//   },
//   delete: async (url: string, data?: any) => {
//     const token = localStorage.getItem("jwt-token");
//     try {
//       const response = await axios.delete(url, {
//         headers: {
//           Authorization: `Bearer ${token}`,
//           "Content-Type": "application/json",
//         },
//         withCredentials: true,
//         data: data,
//       });
//       return { json: response.data };
//     } catch (error) {
//       console.error("API request failed:", error);
//       throw error;
//     }
//   },
// };

// export const dataProvider: DataProvider = {
//   getList: async (resource, { pagination = {}, sort = {}, filter = {} }) => {
//     const { page = 1, perPage = 10 } = pagination;
//     const { field = "id", order = "ASC" } = sort;

//     const idFieldMapping: { [key: string]: string } = {
//       products: "productId",
//       categories: "categoryId",
//     };

//     const idField = idFieldMapping[resource] || "id";

//     const query = {
//       page: page.toString(),
//       pageSize: perPage.toString(),
//       sortBy: field,
//       sortOrder: order,
//       ...filter,
//     };

//     let url: string;
//     if (filter.search) {
//       const keyword = filter.search;
//       delete query.search;
//       url = `${apiUrl}/${resource}/keyword/${encodeURIComponent(keyword)}?${new URLSearchParams(query).toString()}`;
//     } else if (filter.categoryId) {
//       const categoryId = filter.categoryId;
//       delete query.categoryId;
//       url = `${apiUrl}/categories/${categoryId}/${resource}?${new URLSearchParams(query).toString()}`;
//     } else {
//       url = `${apiUrl}/${resource}?${new URLSearchParams(query).toString()}`;
//     }

//     try {
//       const { json } = await httpClient.get(url);
//       const baseUrl = "http://localhost:5155/uploads/";

//       const data = Array.isArray(json)
//         ? json.map((item: any) => ({
//             id: item[idField],
//             ...item,
//             photo: item.photo ? `${baseUrl}${item.photo}` : null,
//           }))
//         : [];

//       return {
//         data,
//         total: json.length || 0,
//       };
//     } catch (error) {
//       console.error("Error fetching list:", error);
//       throw error;
//     }
//   },

//   getOne: async (resource, params: GetOneParams) => {
//     try {
//       const url = `${apiUrl}/${resource}/${params.id}`;
//       const { json } = await httpClient.get(url);

//       const idFieldMapping: { [key: string]: string } = {
//         products: "productId",
//         categories: "categoryId",
//       };
//       const idField = idFieldMapping[resource] || "id";
//       const baseUrl = "http://localhost:5155/uploads/";

//       const data = {
//         id: json[idField],
//         ...json,
//         photo: json.photo ? `${baseUrl}${json.photo}` : null,
//       };

//       return { data };
//     } catch (error) {
//       console.error("Error fetching one:", error);
//       throw error;
//     }
//   },

//   // create: async <RecordType extends RaRecord = any>(resource: string, params: CreateParams<RecordType>) => {
//   //   try {
//   //     const url = `${apiUrl}/${resource}`;
//   //     const requestData = { ...params.data };

//   //     const result = await httpClient.post(url, requestData);

//   //     return { data: { ...requestData, id: result.json.id } as RecordType };
//   //   } catch (error) {
//   //     console.error("Error creating resource:", error);
//   //     throw error;
//   //   }
//   // },
//   create: async <RecordType extends RaRecord = any>(
//     resource: string,
//     params: CreateParams<RecordType>
//   ) => {
//     try {
//       const url = `${apiUrl}/${resource}`;
//       const requestData = { ...params.data };
  
//       // Ghi log dữ liệu yêu cầu để kiểm tra
//       console.log("Tạo tài nguyên với dữ liệu:", requestData);
  
//       const result = await httpClient.post(url, requestData);
  
//       return { data: { ...requestData, id: result.json.id } as RecordType };
//     } catch (error) {
//       if (axios.isAxiosError(error) && error.response) {
//         // Ghi log thông tin lỗi bổ sung từ phản hồi của máy chủ
//         console.error("Dữ liệu phản hồi lỗi:", error.response.data);
//         console.error("Trạng thái phản hồi lỗi:", error.response.status);
//         console.error("Headers phản hồi lỗi:", error.response.headers);
//       } else {
//         console.error("Lỗi khi tạo tài nguyên:", error);
//       }
//       throw error;
//     }
//   },

//   update: async <RecordType extends RaRecord = any>(resource: string, params: UpdateParams<RecordType>) => {
//     try {
//       const url = `${apiUrl}/${resource}/${params.id}`;
//       const { data } = params;

//       const result = await httpClient.put(url, data);

//       return { data: { id: params.id, ...result.json } as RecordType };
//     } catch (error) {
//       console.error("Error updating resource:", error);
//       throw error;
//     }
//   },

//   delete: async <RecordType extends RaRecord = any>(resource: string, params: DeleteParams<RecordType>) => {
//     try {
//       const url = `${apiUrl}/${resource}/${params.id}`;
//       await httpClient.delete(url);
//       return { data: params.previousData as RecordType };
//     } catch (error) {
//       console.error("Error deleting resource:", error);
//       throw error;
//     }
//   },

//   deleteMany: async <RecordType extends RaRecord = any>(resource: string, params: DeleteManyParams) => {
//     try {
//       const deletePromises = params.ids.map((id) => {
//         const url = `${apiUrl}/${resource}/${id}`;
//         return httpClient.delete(url);
//       });
//       await Promise.all(deletePromises);
//       return { data: params.ids };
//     } catch (error) {
//       console.error("Error deleting many resources:", error);
//       throw error;
//     }
//   },

//   getMany: async <RecordType extends RaRecord = any>(resource: string, params: GetManyParams) => {
//     try {
//       const idFieldMapping: { [key: string]: string } = {
//         products: "productId",
//         categories: "categoryId",
//       };
//       const idField = idFieldMapping[resource] || "id";
//       const ids = params.ids.join(",");
//       const url = `${apiUrl}/${resource}?ids=${ids}`;
//       const { json } = await httpClient.get(url);

//       const data = Array.isArray(json)
//         ? json.map((item: any) => ({
//             id: item[idField],
//             ...item,
//           }))
//         : [];

//       return { data };
//     } catch (error) {
//       console.error("Error fetching many:", error);
//       throw error;
//     }
//   },

//   getManyReference: async (resource, params) => {
//     throw new Error("Function not implemented.");
//   },

//   updateMany: async (resource, params) => {
//     throw new Error("Function not implemented.");
//   },
// };
