async function post<TBody, TResult>(url: string, data: TBody) : Promise<TResult> {
    const response = await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data)
    });
    return await response.json();
}

export interface ApiService {
    post: <TBody, TResult>(url: string, data: TBody) => Promise<TResult>; 
}

export const api: ApiService = {
    post: post
};