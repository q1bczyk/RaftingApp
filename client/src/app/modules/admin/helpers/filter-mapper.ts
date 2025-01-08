export function mapFiltersToQueryParams(filters: Record<string, any>): string {
    const params = Object.entries(filters)
      .filter(([_, value]) => value !== undefined && value !== null) 
      .map(([key, value]) => {
        if (value instanceof Date) return `${encodeURIComponent(key)}=${encodeURIComponent(value.toISOString())}`;
        else return `${encodeURIComponent(key)}=${encodeURIComponent(value)}`;
      });
    return decodeURIComponent('?' + params.join("&"));
  }