async function post<TBody, TResult>(url: string, data: TBody): Promise<TResult> {
    const response = await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    if (response.ok) {
        return await response.json();
    }
    
    throw new Error(await response.text());
}

async function get<TResult>(url: string): Promise<TResult> {
    const response = await fetch(url);
    return await response.json();
}

export interface ApiService {
    post: <TBody, TResult>(url: string, data: TBody) => Promise<TResult>;
    get: <TResult>(url: string) => Promise<TResult>;
}

export const api: ApiService = { post, get };