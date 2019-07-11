import { api } from './api';

describe('api', () => {
    it('should post data to url', async () => {
        fetchMock.mockResponse('{ "name": "bob" }');

        const result = await api.post('https://something.com/data', { id: 3 });
        expect(result).toEqual({ name: 'bob' });
        expect(fetch).toHaveBeenCalledWith('https://something.com/data', {
            method: 'POST',
            body: JSON.stringify({ id: 3 }),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    });

    it('should get data from ur', async () => {
        fetchMock.mockResponse('{"id": 6 }');

        const result = await api.get('https://somwhere.com/bob');
        expect(result).toEqual({ id: 6 });
    });
});
