namespace SessionAuthentication.Utils;

public class SessionData
{
    public required string Username;
    public required string RefreshToken;
    public required string ExpiresIn;
}

public class TokenData
{
    public required string AccessToken;
    public required string RefreshToken;
}

public class Session
{
    private const int SessionLength = 5; // In minutes
    
    private readonly Dictionary<string, string> _tokens = new Dictionary<string, string>();
    private readonly Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();
    private readonly Dictionary<string, SessionData> _sessions = new Dictionary<string, SessionData>();
    public TokenData CreateSession(string username)
    {
        // Create expiration date
        var currentTime = DateTimeOffset.Now;
        var expiresIn = currentTime.AddMinutes(SessionLength);
        var expiresInUnix = expiresIn.ToUnixTimeSeconds();
        
        // Check if session already exists
        if (SessionExistsByUsername(username))
        {
            DeleteSession(_tokens[username]);
        }
        
        // Generate session token
        var accessToken = StringUtils.GenerateRandomString(64);
        var refreshToken = StringUtils.GenerateRandomString(64);
        
        
        // Create new session object
        var newSession = new SessionData
        {
            Username = username,
            RefreshToken = refreshToken,
            ExpiresIn = expiresInUnix.ToString(),
        };

        
        // Add session to dictionary
        _refreshTokens.Add(refreshToken, accessToken);
        _sessions.Add(accessToken, newSession);
        _tokens.Add(username, accessToken);

        return new TokenData()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public SessionData GetSession(string token)
    {
        var session = _sessions[token];
        return session;
    }

    private bool SessionExists(string token)
    {
        return _sessions.ContainsKey(token);
    }
    
    private bool RefreshTokenExists(string refreshToken)
    {
        return _refreshTokens.ContainsKey(refreshToken);
    }
    
    private bool SessionExistsByUsername(string username)
    {
        return _tokens.ContainsKey(username);
    }

    public TokenData RefreshSession(string refreshToken)
    {
        // Check if refresh token exists
        if (!RefreshTokenExists(refreshToken))
        {
            return null!;
        }
        
        // Get session token
        var sessionToken = _refreshTokens[refreshToken];
        var session = GetSession(sessionToken);
        
        // Check if session exists
        if (!SessionExists(sessionToken))
        {
            return null!;
        }
        
        var username = session.Username;
        
        // Delete old session
        DeleteSession(sessionToken);
        return CreateSession(username);
    }

    private void DeleteSession(string token)
    {
        // Get session
        var session = GetSession(token);
        
        // Delete session
        _sessions.Remove(token);
        _tokens.Remove(session.Username);
        _refreshTokens.Remove(session.RefreshToken);
    } 
}