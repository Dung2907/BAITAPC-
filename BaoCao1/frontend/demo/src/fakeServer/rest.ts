// import { FetchMockAdapter, withDelay } from 'fakerest';
// import fetchMock from 'fetch-mock';
// import generateData from 'data-generator-retail';

// export default () => {
//     const data = generateData();
//     const adapter = new FetchMockAdapter({
//         baseUrl: 'http://localhost:4000',
//         data,
//         loggingEnabled: true,
//         middlewares: [withDelay(500)],
//     });
//     if (window) {
//         window.restServer = adapter.server; // give way to update data in the console
//     }
//     fetchMock.mock('begin:http://localhost:4000', adapter.getHandler());
//     return () => fetchMock.restore();
// };
import { FetchMockAdapter, withDelay } from 'fakerest';
import fetchMock from 'fetch-mock';
import generateData from 'data-generator-retail';

const mockData = generateData(); // Giả sử generateData() trả về dữ liệu giả cho products và các điểm cuối khác

export default () => {
    const adapter = new FetchMockAdapter({
        baseUrl: 'http://localhost:5129/api',
        data: mockData,
        loggingEnabled: true,
        middlewares: [withDelay(500)],
    });

    // Thiết lập các điểm cuối mock với URL cơ sở mới
    fetchMock.mock('begin:http://localhost:5129/api', adapter.getHandler());

    // Xử lý các điểm cuối cụ thể với URL cơ sở đã cập nhật
    fetchMock
        .mock('http://localhost:5129/api/Categories', {
            body: mockData.categories,
            headers: { 'Content-Type': 'application/json' },
        })
        .mock('http://localhost:5129/api/ChildCategories', {
            body: mockData.childCategories,
            headers: { 'Content-Type': 'application/json' },
        })
        .mock('http://localhost:5129/api/Brands', {
            body: mockData.brands,
            headers: { 'Content-Type': 'application/json' },
        })
        .mock('http://localhost:5129/api/Products', {
            body: mockData.products, // Thêm dữ liệu giả cho sản phẩm
            headers: { 'Content-Type': 'application/json' },
        });

    if (window) {
        window.restServer = adapter.server; // Cho phép cập nhật dữ liệu trong console
    }

    return () => fetchMock.restore();
};

