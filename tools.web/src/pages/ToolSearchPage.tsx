import { useState } from 'react';
import { searchTools } from '../api/toolsApi';
import type { ToolDetailsDto } from '../api/toolsApi';
import ToolGrid from '../components/ToolGrid';

export default function ToolSearchPage() {
    const [name, setName] = useState('');
    const [category, setCategory] = useState('');
    const [description, setDescription] = useState('');
    const [results, setResults] = useState<ToolDetailsDto[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [searched, setSearched] = useState(false);
    const [page] = useState(1);
    const [pageSize] = useState(10);

    const handleSearch = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        setSearched(false);
        try {
            const tools = await searchTools({ name, category, description, page, pageSize });
            setResults(Array.isArray(tools) ? tools : []);
            setSearched(true);
        } catch (err: any) {
            setError(err.message || 'Search failed');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={{ maxWidth: 600, margin: '2rem auto' }}>
            <h2>Search Tools</h2>
            <form onSubmit={handleSearch} style={{ display: 'flex', gap: 12, marginBottom: 24 }}>
                <input
                    type="text"
                    placeholder="Name"
                    value={name}
                    onChange={e => setName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Category"
                    value={category}
                    onChange={e => setCategory(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Description"
                    value={description}
                    onChange={e => setDescription(e.target.value)}
                />
                <button
                    type="submit"
                    disabled={loading || (!name && !category && !description)}
                    style={{ minWidth: 100 }}
                >
                    {loading ? 'Searching...' : 'Search'}
                </button>
            </form>
            {error && <div style={{ color: 'red', marginBottom: 12 }}>{error}</div>}
            {searched && !loading && results.length === 0 && (
                <div style={{ color: '#888', marginBottom: 12 }}>No tools found for your search.</div>
            )}
            {searched && !loading && results.length > 0 && (
                <ToolGrid tools={results} />
            )}
        </div>
    );
}
