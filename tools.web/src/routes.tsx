import { BrowserRouter, Route, Routes, Navigate} from 'react-router-dom';
import ToolListPage from './pages/ToolListPage';
import ToolCreatePage from './pages/ToolCreatePage';
import ToolDetailsPage from './pages/ToolDetailsPage';
import ToolEditPage from './pages/ToolEditPage';
import SideNav from './components/SideNav';
import ToolSearchPage from './pages/ToolSearchPage';

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <div style={{ display: 'flex' }}>
                <SideNav />
                <div style={{ marginLeft: 220, flex: 1, minHeight: '100vh' }}>
                    <Routes>
                        <Route path="/" element={<Navigate to="/tools/list" replace />} />
                        <Route path="/tools/list" element={<ToolListPage />} />
                        <Route path="/tools/new" element={<ToolCreatePage />} />
                        <Route path="/tools/search" element={<ToolSearchPage />} />
                        <Route path="/tools/:id" element={<ToolDetailsPage />} />
                        <Route path="/tools/:id/edit" element={<ToolEditPage />} />
                    </Routes>
                </div>
            </div>
        </BrowserRouter>
    );
}
