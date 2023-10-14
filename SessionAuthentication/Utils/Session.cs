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

public static class Session
{
    private const int SessionLength = 5; // In minutes
    
    private static Dictionary<string, string> Tokens { get; } = new Dictionary<string, string>();
    private static Dictionary<string, string> RefreshTokens { get; } = new Dictionary<string, string>();
    private static Dictionary<string, SessionData> Sessions { get; } = new Dictionary<string, SessionData>();

    public static TokenData CreateSession(string username)
    {
        // Create expiration date
        var currentTime = DateTimeOffset.Now;
        var expiresIn = currentTime.AddMinutes(SessionLength);
        var expiresInUnix = expiresIn.ToUnixTimeSeconds();
        
        // Check if session already exists
        if (SessionExistsByUsername(username))
        {
            DeleteSession(Tokens[username]);
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
        RefreshTokens.Add(refreshToken, accessToken);
        Sessions.Add(accessToken, newSession);
        Tokens.Add(username, accessToken);

        return new TokenData()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public static SessionData GetSession(string token)
    {
        var session = Sessions[token];
        return session;
    }

    public static bool SessionExists(string token)
    {
        return Sessions.ContainsKey(token);
    }
    
    private static bool RefreshTokenExists(string refreshToken)
    {
        return RefreshTokens.ContainsKey(refreshToken);
    }
    
    private static bool SessionExistsByUsername(string username)
    {
        return Tokens.ContainsKey(username);
    }

    public static TokenData RefreshSession(string refreshToken)
    {
        // Check if refresh token exists
        if (!RefreshTokenExists(refreshToken))
        {
            return null!;
        }
        
        // Get session token
        var sessionToken = RefreshTokens[refreshToken];
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

    private static void DeleteSession(string token)
    {
        // Get session
        var session = GetSession(token);
        
        // Delete session
        Sessions.Remove(token);
        Tokens.Remove(session.Username);
        RefreshTokens.Remove(session.RefreshToken);
    } 
}