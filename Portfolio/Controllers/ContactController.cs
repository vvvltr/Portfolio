﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Entity;
using Portfolio.Misc.Services;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;
    private readonly EmailSender _emailSender;
    private readonly EmailConfiguration _emailConfiguration;

    public ContactController(ILogger<ContactController> logger, EmailSender emailSender, EmailConfiguration emailConfiguration)
    {
        _logger = logger;
        _emailSender = emailSender;
        _emailConfiguration = emailConfiguration;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Send()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Send(Request requestModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                 _emailSender.SendEmail(requestModel.MessageBody, requestModel.Subject, requestModel.YourEmail);
            }
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.Message);
        }
        return View("success");
    }
}