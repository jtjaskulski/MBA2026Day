$bestIP = (Get-NetIPAddress -AddressFamily IPv4 | 
    Where-Object { $_.IPAddress -notmatch '^(127\.|169\.254\.)' } | 
    Select-Object -First 1).IPAddress

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "Your IP: $bestIP" -ForegroundColor Green
Write-Host "Use in config.ts:" -ForegroundColor Yellow
Write-Host "  return 'http://${bestIP}:5000/api';" -ForegroundColor White
Write-Host "==================================" -ForegroundColor Cyan

# Test connection
try {
    $response = Invoke-WebRequest -Uri "http://${bestIP}:5000/api/items" -TimeoutSec 3;
    Write-Host "[OK] API is responding!" -ForegroundColor Green
}
catch {
    Write-Host "[FAIL] Cannot connect to API" -ForegroundColor Red
}