import polyglotI18nProvider from 'ra-i18n-polyglot';
import {
    Admin,
    CustomRoutes,
    Resource,
    localStorageStore,
    useStore,
    StoreContextProvider,
} from 'react-admin';
import { Navigate, Route } from 'react-router';

import {authProvider} from './authProvider';
import { CategoryEdit } from "./categories/CategoryEdit";
import { CategoryList } from "./categories/CategoryList";
import { CategoryCreate } from './categories/CategoryCreate';
import  ProductEdit  from "./products/ProductEdit";
import products from "./products"
import { Dashboard } from './dashboard';
// import {dataProvider} from './dataProvider';
import englishMessages from './i18n/en';
import invoices from './invoices';
import { Layout, Login, Register } from './layout';
import orders from './orders';

import reviews from './reviews';
import Segments from './segments/Segments';
import visitors from './visitors';
import { themes, ThemeName } from './themes/themes';
import ProductCreate from './products/ProductCreate';
import { ProductList } from './products/ProductList';
import ProductDetails from './products/ProductDetails';
import CategoryDetails from './categories/CategoryDetail';

const i18nProvider = polyglotI18nProvider(
    locale => {
        if (locale === 'fr') {
            return import('./i18n/fr').then(messages => messages.default);
        }

        // Always fallback on english
        return englishMessages;
    },
    'en',
    [
        { locale: 'en', name: 'English' },
        { locale: 'fr', name: 'Français' },
    ]
);

const store = localStorageStore(undefined, 'ECommerce');

const App = () => {
    const [themeName] = useStore<ThemeName>('themeName', 'soft');
    const lightTheme = themes.find(theme => theme.name === themeName)?.light;
    const darkTheme = themes.find(theme => theme.name === themeName)?.dark;
    return (
        <Admin
            title=""
            // dataProvider={dataProvider}
            store={store}
            authProvider={authProvider}
            dashboard={Dashboard}
            loginPage={Login}
            
            layout={Layout}
            i18nProvider={i18nProvider}
            disableTelemetry
            lightTheme={lightTheme}
            darkTheme={darkTheme}
            defaultTheme="light"
        >
            <CustomRoutes>
                <Route path="/segments" element={<Segments />} />
                <Route path="/products/edit/:productId" element={<ProductEdit />} />
                <Route path="/products/detail/:productId" element={<ProductDetails />} />
                <Route path="/categories/edit/:categoryId" element={<CategoryEdit />} />
                <Route path="/categories/detail/:categoryId" element={<CategoryDetails />} />
                <Route path="/register" element={<Register />} /> 
            </CustomRoutes>
            <Resource name="customers" {...visitors} />
            <Resource name="orders" {...orders} />
            <Resource name="invoices" {...invoices} />
            <Resource name="products" list={ProductList} create={ProductCreate} edit={ProductEdit}  />
            <Resource name="categories" list={CategoryList} create={CategoryCreate} edit={CategoryEdit}  />
            <Resource name="reviews" {...reviews} />
           
        </Admin>
    );
};

const AppWrapper = () => (
    <StoreContextProvider value={store}>
        <App />
    </StoreContextProvider>
);

export default AppWrapper;
