import { useEffect, useState } from 'react';
import { getTools, type ToolDetailsDto } from '../api/toolsApi';
import ToolGrid from '../components/ToolGrid';

export default function ToolListPage() {
    const [tools, setTools] = useState<ToolDetailsDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [search, setSearch] = useState('');
    const [filtered, setFiltered] = useState<ToolDetailsDto[]>([]);

    useEffect(() => {
        setLoading(true);
        getTools()
            .then(setTools)
            .catch((err) => setError(err.message))
            .finally(() => setLoading(false));
    }, []);

    useEffect(() => {
        const visibleTools = tools.filter(t => !t.isHidden);
        if (!search.trim()) {
            setFiltered(visibleTools);
        } else {
            const s = search.trim().toLowerCase();
            setFiltered(
                visibleTools.filter(
                    (t) =>
                        t.name?.toLowerCase().includes(s) ||
                        t.description?.toLowerCase().includes(s) ||
                        t.category?.some((cat) => cat.toLowerCase().includes(s))
                )
            );
        }
    }, [search, tools]);

    if (loading) return <div>Loading tools...</div>;
    if (error) return <div style={{ color: 'red' }}>{error}</div>;

    return (
        <div>
            <div style={{ maxWidth: 400, margin: '2rem auto 1rem auto' }}>
                <input
                    type="text"
                    placeholder="Filter tools by name, description, or category..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                    style={{ width: '100%', padding: 8, fontSize: 16 }}
                />
            </div>
            <ToolGrid tools={filtered} />
        </div>
    );
}
